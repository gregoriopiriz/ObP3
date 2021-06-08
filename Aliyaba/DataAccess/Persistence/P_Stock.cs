using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_Stock
    {
        public static void AddStock(DTO_Stock DTO_S)
        {
            Stock S = Mappers.M_Stock.MapToEntity(DTO_S);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.Stocks.Add(S);

                        context.SaveChanges();
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
        }
        public static void DeleteStock(int _ID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Stock P = context.Stocks.FirstOrDefault(f => f.ID == _ID);
                        context.Stocks.Remove(P);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
        }
        public static List<DTO_Stock> GetStocks()
        {
            List<Stock> Stocks = new List<Stock>();
            List<DTO_Stock> _Stocks = new List<DTO_Stock>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Stocks= context.Stocks.ToList();
                    }

                    foreach(Stock S in Stocks)
                    {
                        _Stocks.Add(Mappers.M_Stock.MapToDTO(S));
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            return _Stocks;
        }

        public static int GetQuantityOfStockByProductCode(string _ProductCode)
        {
            int _StockCuantity = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        List<Stock> Ss = context.Stocks.Where(w => w.ProductCode == _ProductCode).ToList();
                        _StockCuantity = Ss.Sum(s => s.Quantity) - Ss.Sum(s => s.ReservedQuantity);
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            return _StockCuantity;
        }
    }
}
