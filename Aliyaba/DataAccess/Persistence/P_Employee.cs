using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_Employee
    {
        public static void AddEmployee(DTO_Employee DTO_E)
        {
            Employee E = Mappers.M_Employee.MapToEntity(DTO_E);
            E.isActive = true;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.Employees.Add(E);

                        context.SaveChanges();
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
        }
        public static void DeleteEmployee(string UserName)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Employee E = context.Employees.FirstOrDefault(f => f.UserName == UserName);
                        E.isActive = false;
                        context.SaveChanges();
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
        }

        public static void EditEmployee(DTO_Employee _E)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Employee E = context.Employees.FirstOrDefault(f => f.ID == _E.ID);
                        
                        E.Name = _E.Name;
                        E.Password = _E.Password;
                        E.Surname = _E.Surname;
                        E.PositionName = _E.positionName;
                        E.UserName = _E.UserName;

                        context.SaveChanges();
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
        }

        public static List<DTO_Employee> GetAllEmployees()
        {
            List<Employee> Employees = new List<Employee>();
            using (TransactionScope scope = new TransactionScope())
            {
                List<DTO_Employee> DTO_Employees = new List<DTO_Employee>();

                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Employees = context.Employees.Where(w => w.isActive == true).ToList();
                    }


                    foreach (Employee E in Employees)
                    {
                        if (E.isActive == true)
                        {
                            DTO_Employees.Add(Mappers.M_Employee.MapToDTO(E));
                        }
                    }

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return DTO_Employees;
            }

        }
    }
}
