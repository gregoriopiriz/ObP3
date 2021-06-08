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
    public class H_State
    {
        private static H_State _instance;
        public static H_State getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_State();
            }

            return _instance;
        }

        public string GetCurrentStateNameByOrderID(int OrderID)
        {
            return P_State.GetCurrentStateNameByOrderID(OrderID);
        }

        public bool IsValidData(DTO_State _S)
        {
            if (_S.Name.Length <= 40)
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        if (context.States.Any(a => a.Name == _S.Name))
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

        public void AddState(DTO_State _S)
        {
            P_State.AddState(_S);
        }

        public List<DTO_State> GetStatesByOrderID(int iD)
        {
            return P_State.GetStatesByOrderID(iD);
        }

        public void DeleteState(int ID)
        {
            P_State.DeleteState(ID);
        }
    }
}
