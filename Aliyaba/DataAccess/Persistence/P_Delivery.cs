using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_Delivery
    {
        public static void AddDelivery(DTO_Delivery DTO_D)
        {
            Delivery D = Mappers.M_Delivery.MapToEntity(DTO_D);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.Deliveries.Add(D);

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
        public static void DeleteDelivery(int _ID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Delivery D = context.Deliveries.FirstOrDefault(d => d.ID == _ID);
                        context.Deliveries.Remove(D);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
        }

        public static List<DTO_Delivery> GetDeliveriesToDeliver()
        {
            List<Delivery> Deliveries = new List<Delivery>();
            List<Order> Orders = new List<Order>();
            List<DTO_Order> _Orders = new List<DTO_Order>();
            using (TransactionScope scope = new TransactionScope())
            {
                List<DTO_Delivery> _Deliveries = new List<DTO_Delivery>();

                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Deliveries = context.Deliveries.ToList();
                        Orders = context.Orders.Where(w => w.States.OrderByDescending(o => o.Date).FirstOrDefault().Name == "Listo para Entregar").ToList();
                    }


                    foreach (Delivery D in Deliveries)
                    {
                        List<Order> OrdersAux = Orders.Where(w => w.DeliveryID == D.ID).ToList();
                        if (OrdersAux.Count > 0)
                        {
                            _Deliveries.Add(Mappers.M_Delivery.MapToDTO(D));
                        }
                    }

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return _Deliveries;
            }
        }

        public static List<DTO_Delivery> GetAllDeliveries()
        {
            List<Delivery> Deliveries = new List<Delivery>();
            using (TransactionScope scope = new TransactionScope())
            {
                List<DTO_Delivery> DTO_Deliveries = new List<DTO_Delivery>();

                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Deliveries = context.Deliveries.ToList();
                    }


                    foreach (Delivery D in Deliveries)
                    {
                        DTO_Deliveries.Add(Mappers.M_Delivery.MapToDTO(D));
                    }

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return DTO_Deliveries;
            }
        }

        public static int GetIdDelivery(int idEmployee, DateTime commitDate)
        {
            int idDelivery = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Delivery D = context.Deliveries.Where(w => w.EmployeeID == idEmployee).OrderByDescending(o => o.CommitDate).FirstOrDefault();
                        idDelivery = D.ID;
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return idDelivery;
            }

        }
    }
}
