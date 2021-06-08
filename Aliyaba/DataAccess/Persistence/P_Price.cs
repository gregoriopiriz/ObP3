using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_Price
    {

        public static List<DTO_Price> GetPrices()
        {
            List<DTO_Price> DTO_Prices = new List<DTO_Price>();
            List<Price> Prices = new List<Price>();

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                using (AliyabaEntities context = new AliyabaEntities())
                {
                    Prices = context.Prices.ToList();
                }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }

            foreach (Price P in Prices)
            {
                DTO_Prices.Add(Mappers.M_Price.MapToDTO(P));
            }

            return DTO_Prices;
        }


        public static void AddPrice(DTO_Price DTO_P)
        {
            Price P = Mappers.M_Price.MapToEntity(DTO_P);
            P.Date = DateTime.Now;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.Prices.Add(P);

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

        public static DTO_Price GetPriceByID(int _ID)
        {
            Price P = new Price();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        P = context.Prices.FirstOrDefault(f => f.ID == _ID);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }

            return Mappers.M_Price.MapToDTO(P);
        }
    }
}
