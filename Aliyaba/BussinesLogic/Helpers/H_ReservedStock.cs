using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Persistence;

namespace BussinesLogic.Helpers
{
    public class H_ReservedStock
    {
        private static H_ReservedStock _instance;
        public static H_ReservedStock getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_ReservedStock();
            }

            return _instance;
        }

        public void FinishPrepare(int _OrderID)
        {
            P_ReservedStock.FinishPrepare(_OrderID);
        }
    }
}
