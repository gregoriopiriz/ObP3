using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTOs;

namespace DataAccess.Persistence
{
    public class P_User
    {
        public static bool CustomerLogIn(DTO_LogIn LI)
        {
            using (AliyabaEntities context = new AliyabaEntities() )
            {
                if (context.Customers.Any(w => w.UserName == LI.UserName && w.Password == LI.Password))
                {
                    return true;
                }
            }
            return false;
        }
        public static DTO_Employee EmployeeLogIn(DTO_LogIn LI)
        {
            DTO_Employee _E = new DTO_Employee();
            Employee E = new Employee();

            using (AliyabaEntities context = new AliyabaEntities() )
            {
                if (context.Employees.Any(w => w.UserName == LI.UserName && w.Password == LI.Password))
                {
                    E = context.Employees.Where(w => w.isActive == true).FirstOrDefault(w => w.UserName == LI.UserName && w.Password == LI.Password);
                }
            }

            _E = Mappers.M_Employee.MapToDTO(E);

            return _E;
        }
    }
}
