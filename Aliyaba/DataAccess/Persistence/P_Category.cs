using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTOs;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_Category
    {

        public static void AddCategory(DTO_Category DTO_C)
        {
            Category C = Mappers.M_Category.MapToEntity(DTO_C);
            C.isActive = true;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.Categories.Add(C);

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
        public static void DeleteCategory(int _ID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Category C = context.Categories.FirstOrDefault(f => f.ID == _ID);

                        C.isActive = false;

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
        public static void UpdateCategory(DTO_Category _DTO_C)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Category C = context.Categories.FirstOrDefault(f => f.ID == _DTO_C.ID);

                        C.Name = _DTO_C.Name;

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

        public static List<DTO_Category> GetAllCategories()
        {
            List<Category> Categories = new List<Category>();
            using (TransactionScope scope = new TransactionScope())
            {
                List<DTO_Category> DTO_Categories = new List<DTO_Category>();

                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Categories = context.Categories.Where(w=> w.isActive == true).ToList();
                    }


                    foreach (Category C in Categories)
                    {
                        DTO_Categories.Add(Mappers.M_Category.MapToDTO(C));
                    }

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return DTO_Categories;
            }

        }

        public static DTO_Category GetCategoryByID(int _ID)
        {
            Category C = new Category();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        C = context.Categories.Where(w => w.isActive == true).FirstOrDefault(f => f.ID == _ID);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }

            return Mappers.M_Category.MapToDTO(C);
        }

        public static string GetCategoryNameByID(int _ID)
        {
            Category C = new Category();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        C = context.Categories.Where(w => w.isActive == true).FirstOrDefault(f => f.ID == _ID);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
            return C.Name;
        }
    }
}
