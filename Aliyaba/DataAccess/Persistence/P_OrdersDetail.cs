using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_OrdersDetail
    {

        public static void AddOrdersDetail(DTO_OrdersDetail DTO_OD)
        {
            OrdersDetail OD = Mappers.M_OrdersDetail.MapToEntity(DTO_OD);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        List<Stock> Stocks = context.Stocks.Where(w => w.ProductCode == DTO_OD.ProductCode).ToList();
                        if (Stocks.Sum(s => s.Quantity) - Stocks.Sum(s => s.ReservedQuantity) >= DTO_OD.Quantity)
                        {
                            Stock St = Stocks.FirstOrDefault(f => f.Quantity - f.ReservedQuantity >= DTO_OD.Quantity);
                            if (St != null)
                            {
                                St.ReservedQuantity += DTO_OD.Quantity;

                                context.OrdersDetails.Add(OD);

                                context.SaveChanges();
                            }
                            else
                            {
                                int QuantityLeft = DTO_OD.Quantity;

                                foreach (Stock S in Stocks)
                                {
                                    if (QuantityLeft > 0)
                                    {
                                        if (S.Quantity - S.ReservedQuantity > 0)
                                        {
                                            int QuantityAvailable = S.Quantity - S.ReservedQuantity;
                                            if (QuantityAvailable <= QuantityLeft)
                                            {
                                                S.ReservedQuantity += QuantityAvailable;
                                                QuantityLeft -= QuantityAvailable;
                                            }
                                            else if (QuantityAvailable >= QuantityLeft)
                                            {
                                                S.ReservedQuantity += QuantityLeft;
                                                QuantityLeft -= QuantityLeft; 
                                            }
                                        }
                                    }
                                }
                                context.OrdersDetails.Add(OD);
                                context.SaveChanges();
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
        }
        public static void DeleteOrdersDetail(int _ID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        OrdersDetail OD = context.OrdersDetails.FirstOrDefault(f => f.ID == _ID);

                        context.OrdersDetails.Remove(OD);

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
        public static List<DTO_OrdersDetail> GetOrdersDetailByOrderID(int _ID)
        {
            List<DTO_OrdersDetail> _OD = new List<DTO_OrdersDetail>();
            List<OrdersDetail> OD = new List<OrdersDetail>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        OD = context.OrdersDetails.Where(w => w.OrderID == _ID).ToList();
                    }

                    foreach (OrdersDetail OrD in OD)
                    {
                        _OD.Add(Mappers.M_OrdersDetail.MapToDTO(OrD));
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            return _OD;
        }
    }
}
