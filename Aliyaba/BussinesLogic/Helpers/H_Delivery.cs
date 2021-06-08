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
    public class H_Delivery
    {
        private static H_Delivery _instance;
        public static H_Delivery getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_Delivery();
            }

            return _instance;
        }

        public bool IsValidData(DTO_Delivery _D)
        {
            try
            {
                if (_D.CommitDate == null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

            }

            return true;
        }

        public void AddDelivery(DTO_Delivery _D)
        {

                P_Delivery.AddDelivery(_D);
            
        }

        public void DeleteDelivery(int _ID)
        {
            P_Delivery.DeleteDelivery(_ID);
        }

        public List<DTO_Delivery> GetDeliveriesToDeliver()
        {
            return P_Delivery.GetDeliveriesToDeliver();
        }
        public List<DTO_Delivery> GetAllDeliveries()
        {
            return P_Delivery.GetAllDeliveries();
        }
        public int GetIdDelivery(int idEmployee, DateTime commitDate)
        {
            return P_Delivery.GetIdDelivery(idEmployee, commitDate);
        }
    }

}
