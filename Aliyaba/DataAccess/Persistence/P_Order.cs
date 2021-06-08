using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_Order
    {
        public static void AddOrder(DTO_Order DTO_O)
        {
            Order O = Mappers.M_Order.MapToEntity(DTO_O);
            O.IsActive = true;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.Orders.Add(O);

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

        public static List<DTO_Order> GetToDeliverOrders()
        {
            List<DTO_Order> _O = new List<DTO_Order>();
            List<Order> O = new List<Order>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        O = context.Orders.Where(w => w.States.OrderByDescending(o => o.Date).FirstOrDefault().Name == "Preparado" && w.IsActive == true).ToList();
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            foreach (Order Or in O)
            {
                _O.Add(Mappers.M_Order.MapToDTO(Or));
            }
            return _O;
        }

        public static List<DTO_Order> GetDeliveredOrders()
        {
            List<DTO_Order> _O = new List<DTO_Order>();
            List<Order> O = new List<Order>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        O = context.Orders.Where(w => w.States.OrderByDescending(o => o.Date).FirstOrDefault().Name == "Listo para Entregar" || w.States.OrderByDescending(o => o.Date).FirstOrDefault().Name == "Entregado" || w.States.OrderByDescending(o => o.Date).FirstOrDefault().Name == "En Reparto").ToList();
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            foreach (Order Or in O)
            {
                _O.Add(Mappers.M_Order.MapToDTO(Or));
            }
            return _O;
        }

        public static List<DTO_Order> GetAllOrders()
        {
            List<Order> Orders = new List<Order>();
            using (TransactionScope scope = new TransactionScope())
            {
                List<DTO_Order> DTO_Orders = new List<DTO_Order>();

                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Orders = context.Orders.ToList();
                    }


                    foreach (Order O in Orders)
                    {
                        DTO_Orders.Add(Mappers.M_Order.MapToDTO(O));
                    }

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return DTO_Orders;
            }


        }

        public static List<DTO_Location> GetDeliveredOrderLocations()
        {
            List<DTO_Location> Locs = new List<DTO_Location>();
            List<int?> IDLocs = new List<int?>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        IDLocs = context.Orders.Where(w => w.States.OrderByDescending(o => o.Date).FirstOrDefault().Name == "Entregado").Select(s => s.LocationID).ToList();
                    }

                    foreach (int ID in IDLocs)
                    {
                        Locs.Add(P_Location.GetLocationByID(ID));
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            return Locs;
        }

        public static List<DTO_Order> GetOrdersByDeliveryID(int _idD)
        {
            List<DTO_Order> _O = new List<DTO_Order>();
            List<Order> O = new List<Order>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        O = context.Orders.Where(w => w.DeliveryID == _idD && w.IsActive == true).ToList();
                    }

                    foreach (Order Or in O)
                    {
                        DTO_Order _or = Mappers.M_Order.MapToDTO(Or);
                        List<State> states = new List<State>();
                        List<DTO_State> _states = new List<DTO_State>();
                        using (AliyabaEntities context = new AliyabaEntities())
                        {
                            states = context.States.Where(w => w.OrderID == Or.ID).OrderByDescending(o => o.Date).ToList();
                        }

                        foreach (State s in states)
                        {
                            _states.Add(Mappers.M_State.MapToDTO(s));
                        }

                        _or.States = _states;
                        _or.CurrentStateID = _states.FirstOrDefault().ID;

                        _O.Add(_or);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            return _O;
        }

        public static void AddDeliveryIdToOrder(int orderID, int deliveryID, int EmployeeID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.Orders.FirstOrDefault(f => f.ID == orderID).DeliveryID = deliveryID;
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

        public static void CancelOrder(int _ID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    State S = new State();
                    S.OrderID = _ID;
                    S.Date = DateTime.Now;
                    S.Name = "Cancelado";

                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        string Sname = context.States.OrderByDescending(o => o.Date).FirstOrDefault(f => f.OrderID == _ID).Name;
                        if (Sname == "Pendiente" || Sname == "En Preparación")
                        {
                            List<OrdersDetail> OD = context.OrdersDetails.Where(w => w.OrderID == _ID).ToList();

                            foreach (OrdersDetail od in OD)
                            {
                                Stock s = new Stock();
                                s = context.Stocks.FirstOrDefault(f => f.ProductCode == od.ProductCode);
                                s.ReservedQuantity -= od.Quantity;
                                context.SaveChanges();
                            }

                            context.States.Add(S);
                            context.SaveChanges();
                        }
                        if (Sname == "Listo para Entregar")
                        {
                            List<ReservedStock> RS = context.ReservedStocks.Where(w => w.OrderID == _ID).ToList();

                            foreach (ReservedStock rs in RS)
                            {
                                Stock s = new Stock();
                                s = context.Stocks.FirstOrDefault(f => f.ID == rs.StockID);
                                s.Quantity += rs.Quantity;
                                context.SaveChanges();
                            }

                            context.States.Add(S);
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

        public static DTO_Order GetLastOrderByUserName(string _UserName)
        {
            DTO_Order _O = new DTO_Order();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        _O = Mappers.M_Order.MapToDTO(context.Orders.OrderByDescending(o => o.EntryDate).FirstOrDefault(f => f.CustomerUsername == _UserName));
                    }


                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            return _O;
        }
        public static List<DTO_Order> GetOrdersByUserName(string _UserName)
        {
            List<DTO_Order> _O = new List<DTO_Order>();
            List<Order> O = new List<Order>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        O = context.Orders.Where(w => w.CustomerUsername == _UserName && w.IsActive == true).ToList();
                    }

                    foreach (Order Or in O)
                    {
                        DTO_Order _or = Mappers.M_Order.MapToDTO(Or);
                        List<State> states = new List<State>();
                        List<DTO_State> _states = new List<DTO_State>();
                        using (AliyabaEntities context = new AliyabaEntities())
                        {
                            states = context.States.Where(w => w.OrderID == Or.ID).OrderByDescending(o => o.Date).ToList();
                        }

                        foreach (State s in states)
                        {
                            _states.Add(Mappers.M_State.MapToDTO(s));
                        }

                        _or.States = _states;
                        _or.CurrentStateID = _states.FirstOrDefault().ID;

                        _O.Add(_or);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            return _O;
        }

        public static DTO_Order GetOrderByID(int _ID)
        {
            DTO_Order _O = new DTO_Order();
            Order O = new Order();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        O = context.Orders.FirstOrDefault(w => w.ID == _ID && w.IsActive == true);
                    }

                    _O = Mappers.M_Order.MapToDTO(O);
                    List<State> states = new List<State>();
                    List<DTO_State> _states = new List<DTO_State>();
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        states = context.States.Where(w => w.OrderID == O.ID).OrderByDescending(o => o.Date).ToList();
                    }

                    foreach (State s in states)
                    {
                        _states.Add(Mappers.M_State.MapToDTO(s));
                    }

                    _O.States = _states;
                    _O.CurrentStateID = _states.FirstOrDefault().ID;

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
                return _O;
            }
        }

        public static List<DTO_Order> GetPendingOrders()
        {
            List<DTO_Order> _O = new List<DTO_Order>();
            List<Order> O = new List<Order>();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        O = context.Orders.Where(w => w.States.OrderByDescending(o => o.Date).FirstOrDefault().Name == "Pendiente" && w.IsActive == true).ToList();
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            foreach (Order Or in O)
            {
                DTO_Order _or = Mappers.M_Order.MapToDTO(Or);
                List<State> states = new List<State>();
                List<DTO_State> _states = new List<DTO_State>();
                using (AliyabaEntities context = new AliyabaEntities())
                {
                    states = context.States.Where(w => w.OrderID == Or.ID).OrderByDescending(o => o.Date).ToList();
                }

                foreach (State s in states)
                {
                    _states.Add(Mappers.M_State.MapToDTO(s));
                }

                _or.States = _states;
                _or.CurrentStateID = _states.FirstOrDefault().ID;

                _O.Add(_or);
            }
            return _O;
        }
    }
}
