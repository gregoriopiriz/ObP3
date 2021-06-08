using Common.DTOs;
using DataAccess;
using DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Helpers
{
    public class H_Order
    {
        private static H_Order _instance;
        public static H_Order getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_Order();
            }

            return _instance;
        }

        public List<DTO_Order> GetToDeliverOrders()
        {
            return P_Order.GetToDeliverOrders();
        }

        public bool IsValidData(DTO_Order _O)
        {
            if (_O.CustomerUsername.Length <= 20)
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        if (context.Customers.Any(a => a.UserName == _O.CustomerUsername)) 
                        //lo que hice fue preguntar si hay algun usuario con ese nombre y si hay lo agrega, si no no.
                        //aunque eso seguramente salga del login.... pero bueno en resumen es lo que se me ocurrio despues lo cambiamos
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return false;
        }

        public List<DTO_Order> GetDeliveredOrders()
        {
            return P_Order.GetDeliveredOrders();
        }

        public List<DTO_Order> GetAllOrders()
        {
            return P_Order.GetAllOrders();
        }

        public List<DTO_Location> GetDeliveredOrderLocations()
        {
            return P_Order.GetDeliveredOrderLocations();
        }

        public void AddDeliveryIdToOrder(int _OrderID, int _DeliveryID, int _EmployeeID)
        {
            P_Order.AddDeliveryIdToOrder(_OrderID, _DeliveryID, _EmployeeID);
        }

        public void AddOrder(DTO_Order _O)
        {
            if (IsValidData(_O))
            {
                P_Order.AddOrder(_O);
            }
        }

        public DTO_Order GetOrderByID(int ID)
        {
            return P_Order.GetOrderByID(ID);
        }

        public void CancelOrder(int ID)
        {
            P_Order.CancelOrder(ID);
        }

        public DTO_Order GetLastOrderByUserName(string _UserName)
        {
            return P_Order.GetLastOrderByUserName(_UserName);
        }
        public List<DTO_Order> GetOrdersByUserName(string _UserName)
        {
            return P_Order.GetOrdersByUserName(_UserName);
        }
        public List<DTO_Order> GetPendingOrders()
        {
            return P_Order.GetPendingOrders();
        }

        public List<DTO_Order> GetOrdersByDeliveryID(int idD)
        {
            return P_Order.GetOrdersByDeliveryID(idD);
        }
    }
}
