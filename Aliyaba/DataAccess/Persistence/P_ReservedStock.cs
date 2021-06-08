using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_ReservedStock
    {
        public List<DTO_ReservedStock> GetReservedStocksByID(int _OrderID)
        {
            List<DTO_ReservedStock> _RS = new List<DTO_ReservedStock>();
            List<ReservedStock> RS = new List<ReservedStock>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        RS = context.ReservedStocks.Where(rs => rs.OrderID == _OrderID).ToList();
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                foreach (ReservedStock rs in RS)
                {
                    _RS.Add(Mappers.M_ReservedStock.MapToDTO(rs));
                }

                return _RS;
            }
        }

        public void AddReservedStock(DTO_ReservedStock _RS)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.ReservedStocks.Add(Mappers.M_ReservedStock.MapToEntity(_RS));
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


        public static List<DTO_OrderPrepareDetail> GetOrdersPrepareDetailByOrderID(int _ID)
        {
            List<DTO_OrderPrepareDetail> _OPDList = new List<DTO_OrderPrepareDetail>();

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        List<OrdersDetail> ODs = context.OrdersDetails.Where(w => w.OrderID == _ID).ToList();
                        foreach (OrdersDetail OD in ODs)
                        {
                            int QuantityLeft = OD.Quantity;
                            DTO_OrderPrepareDetail _OPD = new DTO_OrderPrepareDetail();
                            List<Stock> Stocks = context.Stocks.Where(w => w.ProductCode == OD.ProductCode).ToList();

                            foreach (Stock S in Stocks)
                            {
                                int aux = 0;
                                if (context.ReservedStocks.Where(w => w.StockID == S.ID && w.OrderID != _ID && w.IsPrepared == false).Sum(s => (int?)s.Quantity) != null)
                                {
                                    //asdas
                                    aux += context.ReservedStocks.Where(w => w.StockID == S.ID && w.OrderID != _ID && w.IsPrepared == false).Sum(s => s.Quantity);
                                }
                                int StockReservedQuantity = S.ReservedQuantity - aux;
                                if (QuantityLeft > 0)
                                {
                                    if (StockReservedQuantity > 0)
                                    {
                                        if (StockReservedQuantity <= QuantityLeft)
                                        {
                                            DTO_OrderPrepareDetail OrPrDe = new DTO_OrderPrepareDetail();
                                            OrPrDe.Location = S.Location;
                                            OrPrDe.ProductCode = OD.ProductCode;
                                            OrPrDe.Quantity = S.ReservedQuantity;
                                            QuantityLeft -= S.ReservedQuantity;
                                            _OPDList.Add(OrPrDe);

                                            ReservedStock RS = new ReservedStock();
                                            RS.OrderID = _ID;
                                            RS.Quantity = S.ReservedQuantity;
                                            RS.StockID = S.ID;
                                            RS.IsPrepared = false;


                                            context.ReservedStocks.Add(RS);
                                            context.SaveChanges();
                                        }
                                        else if (StockReservedQuantity >= QuantityLeft)
                                        {
                                            DTO_OrderPrepareDetail OrPrDe = new DTO_OrderPrepareDetail();
                                            OrPrDe.Location = S.Location;
                                            OrPrDe.ProductCode = OD.ProductCode;
                                            OrPrDe.Quantity = QuantityLeft;
                                            _OPDList.Add(OrPrDe);


                                            ReservedStock RS = new ReservedStock();
                                            RS.OrderID = _ID;
                                            RS.Quantity = QuantityLeft;
                                            RS.StockID = S.ID;
                                            RS.IsPrepared = false;
                                            QuantityLeft = 0;

                                            context.ReservedStocks.Add(RS);
                                            context.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            return _OPDList;
        }

        public static void FinishPrepare(int _ID)
        {
            List<ReservedStock> RS = new List<ReservedStock>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        RS = context.ReservedStocks.Where(w => w.OrderID == _ID).ToList();
                    }

                    foreach (ReservedStock rs in RS)
                    {
                        using (AliyabaEntities context = new AliyabaEntities())
                        {
                            foreach (Stock s in context.Stocks.Where(w => w.ID == rs.StockID).ToList())
                            {
                                s.Quantity -= rs.Quantity;
                                s.ReservedQuantity -= rs.Quantity;
                            }
                            context.SaveChanges();
                        }
                    }
                    foreach (ReservedStock rs in RS)
                    {
                        using (AliyabaEntities context = new AliyabaEntities())
                        {
                            ReservedStock R = context.ReservedStocks.FirstOrDefault(f => f.ID == rs.ID);
                            R.IsPrepared = true;
                            context.SaveChanges();
                        }
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
        }
    }
}
