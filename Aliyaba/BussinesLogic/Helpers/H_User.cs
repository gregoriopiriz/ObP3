using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTOs;
using DataAccess.Persistence;

namespace BussinesLogic.Helpers
{
    public class H_User
    {
        private static H_User _instance;
        public static H_User getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_User();
            }

            return _instance;
        }


        public bool CustomerLogIn(DTO_LogIn LI)
        {
            return P_User.CustomerLogIn(LI);
        }
        public DTO_Employee EmployeeLogIn(DTO_LogIn LI)
        {
            return P_User.EmployeeLogIn(LI);
        }
    }
}
