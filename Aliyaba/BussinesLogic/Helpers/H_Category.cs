using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Common.DTOs;
using DataAccess;
using DataAccess.Persistence;

namespace BussinesLogic.Helpers
{
    public class H_Category
    {
        private static H_Category _instance;
        public static H_Category getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_Category();
            }

            return _instance;
        }

        public bool IsValidData(DTO_Category _C)
        {
            if (_C.Name.Length > 20)
            {
                return false;
            }

            return true;
        }

        public void AddCategory(DTO_Category _C)
        {
            if (IsValidData(_C))
            {
                P_Category.AddCategory(_C);
            }
        }

        public void DeleteCategory(int ID)
        {
            P_Category.DeleteCategory(ID);
        }

        public List<DTO_Category> GetCategories()
        {
            return P_Category.GetAllCategories();
        }

        public string GetCategoryNameByID(int ID)
        {
            return P_Category.GetCategoryNameByID(ID);
        }

    }
}
