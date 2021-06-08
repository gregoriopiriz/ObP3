using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTOs;
using DataAccess.Persistence;

namespace BussinesLogic.Helpers
{
    public class H_Price
    {
        private static H_Price _instance;
        public static H_Price getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_Price();
            }

            return _instance;
        }


        public List<DTO_Price> GetPrices()
        {
            return P_Price.GetPrices();
        }

        public bool IsValidData(DTO_Price _P)
        {
            try
            {
                if (_P.Price > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public void AddPrice(DTO_Price _P)
        {
            if (IsValidData(_P))
            {
                P_Price.AddPrice(_P);
            }
        }
    }
}
