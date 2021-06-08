using Common.DTOs;
using DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Helpers
{
    public class H_Reports
    {
        private static H_Reports _instance;

        public static H_Reports getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_Reports();
            }

            return _instance;
        }

        public List<DTO_Product> MostSoldProduct()
        {
            return P_Reports.MostSoldProduct();
        }

        public DTO_OrdersAndItemsQuantity OrdersAndItemsQuantity(DateTime Date)
        {
            return P_Reports.OrdersAndItemsQuantity(Date);
        }

        public TimeSpan DeliveredAverage(DateTime Date1, DateTime Date2)
        {
            return P_Reports.DeliveredAverage(Date1, Date2);
        }
    }
}
