using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_Customer
    {
        public static void AddCustomer(DTO_Customer DTO_C)
        {
            Customer C = Mappers.M_Customer.MapToEntity(DTO_C);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.Customers.Add(C);

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
        public static void DeleteCustomer(string _Document, string _DocumentTypeName)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Customer C = context.Customers.FirstOrDefault(f => f.DocumentTypeName == _DocumentTypeName && f.Document == _Document);
                        context.Customers.Remove(C);
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
        public static void UpdateCustomer(Customer _C)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Customer C = context.Customers.FirstOrDefault(f => f.DocumentType == _C.DocumentType && f.Document == _C.Document);

                        C.Document = _C.Document;
                        C.DocumentType = _C.DocumentType;
                        C.DocumentTypeName = _C.DocumentTypeName;
                        C.Email = _C.Email;
                        C.Locations = _C.Locations;
                        C.Name = _C.Name;
                        C.Orders = _C.Orders;
                        C.Password = _C.Password;
                        C.PhoneNumber = _C.PhoneNumber;
                        C.Surname = _C.Surname;
                        C.UserName = _C.UserName;

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

        public static List<DTO_Customer> GetAllCustomers()
        {
            List<Customer> Customers = new List<Customer>();
            using (TransactionScope scope = new TransactionScope())
            {
                List<DTO_Customer> DTO_Customers = new List<DTO_Customer>();

                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Customers = context.Customers.ToList();
                    }


                    foreach (Customer C in Customers)
                    {
                        DTO_Customers.Add(Mappers.M_Customer.MapToDTO(C));
                    }

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return DTO_Customers;
            }

        }

        public static DTO_Customer GetCustomerByDocument(string _Document, DocumentType _DocumentType)
        {
            Customer C = new Customer();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        C = context.Customers.FirstOrDefault(f => f.Document == _Document && f.DocumentType == _DocumentType);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }

            return Mappers.M_Customer.MapToDTO(C);
        }
    }
}
