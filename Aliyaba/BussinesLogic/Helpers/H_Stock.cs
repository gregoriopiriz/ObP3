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
    public class H_Stock
    {
        private static H_Stock _instance;

        public static H_Stock getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_Stock();
            }

            return _instance;
        }

        public bool IsValidData(DTO_Stock _S)
        {
            if (_S.Quantity > 0)
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        if (context.Products.Any(a => a.Code == _S.ProductCode))
                        //para que siempre agregue stock de un producto que exista
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return false;
        }

        public int GetQuantityOfStockByProductCode(string productCode)
        {
            return P_Stock.GetQuantityOfStockByProductCode(productCode);
        }

        public void AddStock(DTO_Stock _S)
        {
            if (IsValidData(_S))
            {
                P_Stock.AddStock(_S);
            }
        }

        public void DeleteStock(int ID)
        {
            P_Stock.DeleteStock(ID);
        }

        public List<DTO_Stock> GetStocks()
        {
            return P_Stock.GetStocks();
        }
    }
}
