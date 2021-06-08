using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_Photo
    {
        public static void AddPhoto(DTO_Photo p)
        {
            Photo P = Mappers.M_Photo.MapToEntity(p);

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.Photos.Add(P);

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

        public static DTO_Photo GetImageByProductCode(string ProductCode)
        {
            Photo P = new Photo();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        P = context.Photos.FirstOrDefault(f=>f.ProductCode == ProductCode);
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }

            return Mappers.M_Photo.MapToDTO(P);
        }
    }
}
