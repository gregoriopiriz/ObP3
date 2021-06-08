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
    public class H_OrdersDetail
    {
        private static H_OrdersDetail _instance;
        public static H_OrdersDetail getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_OrdersDetail();
            }

            return _instance;
        }

        public bool IsValidData(DTO_OrdersDetail _OD)
        {
            if (_OD.Quantity < 0)
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        if (context.OrdersDetails.Any(a => a.OrderID == _OD.OrderID))
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return true;
        }

        public void AddOrdersDetail(DTO_OrdersDetail _OD)
        {
            if (IsValidData(_OD))
            {
                P_OrdersDetail.AddOrdersDetail(_OD);
            }
        }

        public void DeleteOrdersDetail(int ID)
        {
            P_OrdersDetail.DeleteOrdersDetail(ID);
        }

        public List<DTO_OrdersDetail> GetOrdersDetailByOrderID(int _ID)
        {
            return P_OrdersDetail.GetOrdersDetailByOrderID(_ID);
        }

        public List<DTO_OrderPrepareDetail> GetOrdersPrepareDetailByOrderID(int _ID)
        {
            return P_ReservedStock.GetOrdersPrepareDetailByOrderID(_ID);
        }
    }
}
