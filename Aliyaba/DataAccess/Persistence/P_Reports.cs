using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_Reports
    {
        public static List<DTO_Product> MostSoldProduct()
        {
            List<DTO_Product> _MSProducts = new List<DTO_Product>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    List<Product> MSProducts = new List<Product>();
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        MSProducts = context.Products.OrderBy(w => w.OrdersDetails.Count).ToList();
                        foreach (Product msp in MSProducts)
                        {
                            if (msp.OrdersDetails.Count > 0)
                            {
                                _MSProducts.Add(Mappers.M_Product.MapToDTO(msp));
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
            return _MSProducts;//Falta que ordene
        }

        public static DTO_OrdersAndItemsQuantity OrdersAndItemsQuantity(DateTime Date)
        {
            DTO_OrdersAndItemsQuantity OAIQ = new DTO_OrdersAndItemsQuantity();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    List<Order> OrdersAux = new List<Order>();
                    List<Order> Orders = new List<Order>();
                    int ItemsQuantity = 0;
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        OrdersAux = context.Orders.Include("OrdersDetails").Where(w => w.States.OrderByDescending(o => o.Date).FirstOrDefault().Name == "Entregado").ToList();

                        foreach (Order o in OrdersAux)
                        {
                            if (o.States.OrderByDescending(oo => oo.Date).FirstOrDefault().Date.ToString("MM/DD/AA") == Date.ToString("MM/DD/AA"))
                            {
                                Orders.Add(o);
                            }
                        }
                    }

                    foreach (Order o in Orders)
                    {
                        ItemsQuantity += o.OrdersDetails.Sum(s => s.Quantity);
                    }

                    int OrderQuantity = Orders.Count;
                    OAIQ.ItemsQuantity = ItemsQuantity;
                    OAIQ.OrderQuantity = OrderQuantity;

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

            }
            return OAIQ;
        }

        public static List<DTO_Product> ThermicMap()
        {
            List<DTO_Product> _MSProducts = new List<DTO_Product>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    List<Product> MSProducts = new List<Product>();
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        List<string> aux1 = context.OrdersDetails.Where(w => w.Order.States.OrderByDescending(o => o.Date).FirstOrDefault().Name == "Entregado").GroupBy(g => g.ProductCode).Select(s => s.Key).ToList();

                        foreach (var a in aux1)
                        {
                            MSProducts.Add(context.Products.FirstOrDefault(f => f.Code == a));
                        }

                    }

                    foreach (Product msp in MSProducts)
                    {
                        _MSProducts.Add(Mappers.M_Product.MapToDTO(msp));
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            return _MSProducts;
        }

        public static TimeSpan DeliveredAverage(DateTime Date1, DateTime Date2)
        {
            List<TimeSpan> times = new List<TimeSpan>();
            double _average = 0;
            TimeSpan average = new TimeSpan();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        List<Order> OrdersAux = context.Orders.Where(w => w.States.OrderByDescending(o => o.Date).FirstOrDefault().Name == "Entregado").ToList();

                        List<Order> Orders = new List<Order>();

                        foreach (Order o in OrdersAux)
                        {
                            if (o.States.OrderByDescending(oo => oo.Date).FirstOrDefault().Date <= Date2 && o.States.OrderByDescending(oo => oo.Date).LastOrDefault().Date >= Date1)
                            {
                                Orders.Add(o);
                            }
                        }

                        foreach (Order O in Orders)
                        {
                            TimeSpan t = O.States.OrderByDescending(o => o.Date).FirstOrDefault().Date - O.States.OrderByDescending(o => o.Date).LastOrDefault().Date;

                            times.Add(t);
                        }

                    }

                    _average = times.Average(timeSpan => timeSpan.TotalSeconds);

                    average = TimeSpan.FromSeconds(_average);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            return average;
        }
    }
}
