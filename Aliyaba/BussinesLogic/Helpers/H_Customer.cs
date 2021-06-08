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
    public class H_Customer
    {
        private static H_Customer _instance;
        public static H_Customer getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_Customer();
            }

            return _instance;
        }


        public bool IsValidData(DTO_Customer _C)
        {
            if (_C.UserName.Length <= 20 && _C.Password.Length <= 20 && _C.Name.Length <= 20 && _C.Surname.Length <= 20 && _C.Document.Length <= 20 
                && _C.Email.Length <= 30 && _C.PhoneNumber.Length <= 30 && _C.DocumentTypeName.Length <= 20)
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        if (context.Customers.Any(c => c.UserName == _C.UserName))
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

        public void AddCustomer(DTO_Customer _C)
        {
            if (IsValidData(_C))
            {
                P_Customer.AddCustomer(_C);
            }
        }

        public void DeleteCustomer(string _Document, string _DocumentTypeName)
        {
            P_Customer.DeleteCustomer(_Document, _DocumentTypeName);
        }

        public List<DTO_Customer> GetCustomers()
        {
            return P_Customer.GetAllCustomers();
        }

    }
}
