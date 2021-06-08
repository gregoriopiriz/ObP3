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
    public class H_Employee
    {
        private static H_Employee _instance;
        public static H_Employee getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_Employee();
            }

            return _instance;
        }
        public bool IsValidData(DTO_Employee _E)
        {
            if (_E.UserName.Length <= 20)
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        if (context.Employees.Any(a => a.UserName == _E.UserName))
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

        public void AddEmployee(DTO_Employee _E)
        {
            if (IsValidData(_E))
            {
                P_Employee.AddEmployee(_E);
            }
        }

        public void DeleteEmployee(string UserName)
        {
            P_Employee.DeleteEmployee(UserName);
        }

        public List<DTO_Employee> GetEmployees()
        {
            return P_Employee.GetAllEmployees();
        }

        public void EditEmployee(DTO_Employee E)
        {
            P_Employee.EditEmployee(E);
        }
    }
}
