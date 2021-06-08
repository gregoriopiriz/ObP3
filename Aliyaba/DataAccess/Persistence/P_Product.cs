using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_Product
    {
        public static void AddProduct(DTO_Product DTO_P)
        {
            Product P = Mappers.M_Product.MapToEntity(DTO_P);
            P.isActive = true;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.Products.Add(P);

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
        public static void DeleteProduct(string _Code)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Product P = context.Products.FirstOrDefault(f => f.Code == _Code);
                        P.isActive = false;
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

        public static List<DTO_Product> GetAllProducts()
        {
            List<Product> Products = new List<Product>();
            using (TransactionScope scope = new TransactionScope())
            {
                List<DTO_Product> DTO_Products = new List<DTO_Product>();

                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Products = context.Products.ToList();

                        foreach (Product P in Products)
                        {
                            if (context.Prices.Any(a => a.ProductCode == P.Code) && P.isActive == true)
                            {
                                DTO_Products.Add(Mappers.M_Product.MapToDTO(P));
                            }
                        }
                    }



                    foreach (DTO_Product P in DTO_Products)
                    {
                        using (AliyabaEntities context = new AliyabaEntities())
                        {
                            P.Price = Mappers.M_Price.MapToDTO(context.Prices.OrderByDescending(o => o.Date).FirstOrDefault(f => f.ProductCode == P.Code));
                        }

                    }
                    scope.Complete();

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return DTO_Products;
            }

        }

        public static void EditProduct(DTO_Product _P)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Product P = context.Products.FirstOrDefault(f=>f.Code == _P.Code);

                        P.Description = _P.Description;
                        P.Name = _P.Name;
                        P.CategoryID = _P.CategoryID;

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

        public static List<DTO_Product> GetProducts()
        {
            List<Product> Products = new List<Product>();
            using (TransactionScope scope = new TransactionScope())
            {
                List<DTO_Product> DTO_Products = new List<DTO_Product>();

                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {

                        Products = context.Products.ToList();

                        foreach (Product P in Products)
                        {
                            if (context.Prices.Any(a => a.ProductCode == P.Code) && context.Stocks.Any(aa => aa.ProductCode == P.Code) && P.isActive == true)
                            {
                                DTO_Products.Add(Mappers.M_Product.MapToDTO(P));
                            }
                        }
                    }



                    foreach (DTO_Product P in DTO_Products)
                    {
                        using (AliyabaEntities context = new AliyabaEntities())
                        {
                            P.Price = Mappers.M_Price.MapToDTO(context.Prices.OrderByDescending(o => o.Date).FirstOrDefault(f => f.ProductCode == P.Code));
                        }

                    }
                    scope.Complete();

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return DTO_Products;
            }

        }
        public static DTO_Product GetProductByCode(string _Code)
        {
            DTO_Product P = new DTO_Product();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        P = Mappers.M_Product.MapToDTO(context.Products.FirstOrDefault(f => f.Code == _Code && f.isActive == true));

                        P.Price = Mappers.M_Price.MapToDTO(context.Prices.OrderByDescending(o => o.Date).FirstOrDefault(f => f.ProductCode == P.Code));
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return P;
            }
        }
    }
}
