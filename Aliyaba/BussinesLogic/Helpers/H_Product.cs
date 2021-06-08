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
    public class H_Product
    {
        private static H_Product _instance;
        public static H_Product getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_Product();
            }

            return _instance;
        }

        public bool IsValidData(DTO_Product _P)
        {
            if (_P.Code.Length <= 20)
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        if (context.Products.Any(a => a.Code == _P.Code))
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

        public List<DTO_Product> GetAllProducts()
        {
            return P_Product.GetAllProducts();
        }        
        public List<DTO_Product> GetProducts()
        {
            return P_Product.GetProducts();
        }

        public void AddProduct(DTO_Product _P)
        {
            if (IsValidData(_P))
            {
                P_Product.AddProduct(_P);
            }
        }

        public void DeleteProduct(string Code)
        {
            P_Product.DeleteProduct(Code);
        }


        public DTO_Product GetProductByCode(string _Code)
        {
            return P_Product.GetProductByCode(_Code);
        }

        public void EditProduct(DTO_Product P)
        {
            P_Product.EditProduct(P);
        }
    }
}
