using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Persistence
{
    public class P_Location
    {
        public static void AddLocation(DTO_Location DTO_L)
        {
            Location L = Mappers.M_Location.MapToEntity(DTO_L);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        context.Locations.Add(L);

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
        public static void DeleteLocation(int _ID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Location L = context.Locations.FirstOrDefault(f => f.ID == _ID);
                        context.Locations.Remove(L);
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

        public static List<DTO_Location> GetAllLocations()
        {
            List<Location> Locations = new List<Location>();
            using (TransactionScope scope = new TransactionScope())
            {
                List<DTO_Location> DtoLocations = new List<DTO_Location>();

                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Locations = context.Locations.ToList();
                    }


                    foreach (Location L in Locations)
                    {
                        DtoLocations.Add(Mappers.M_Location.MapToDTO(L));
                    }

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return DtoLocations;
            }
        }

        public static List<DTO_Location> GetLocationsByDeliveryID(int deliveryID)
        {
            List<Location> Locations = new List<Location>();
            List<Order> Orders = new List<Order>();
            using (TransactionScope scope = new TransactionScope())
            {
                List<DTO_Location> DtoLocations = new List<DTO_Location>();

                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Orders = context.Orders.Where(w => w.DeliveryID == deliveryID).ToList();
                    }

                    foreach (Order O in Orders)
                    {
                        using (AliyabaEntities context = new AliyabaEntities())
                        {
                            Locations.Add(context.Locations.FirstOrDefault(f => f.ID == O.LocationID));
                        }
                    }

                    foreach (Location L in Locations)
                    {
                        DtoLocations.Add(Mappers.M_Location.MapToDTO(L));
                    }

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return DtoLocations;
            }
        }

        public static List<DTO_Location> GetLocationsByUser(string UserName)
        {
            List<Location> Locations = new List<Location>();
            using (TransactionScope scope = new TransactionScope())
            {
                List<DTO_Location> DtoLocations = new List<DTO_Location>();

                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        Locations = context.Locations.Where(w => w.CustomerUsername == UserName).ToList();
                    }


                    foreach (Location L in Locations)
                    {
                        DtoLocations.Add(Mappers.M_Location.MapToDTO(L));
                    }

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return DtoLocations;
            }
        }
        public static DTO_Location GetLocationByID(int ID)
        {
            Location OLocation = new Location();
            using (TransactionScope scope = new TransactionScope())
            {
                DTO_Location DtoLocation = new DTO_Location();

                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        OLocation = context.Locations.FirstOrDefault(f => f.ID == ID);
                    }

                    DtoLocation = Mappers.M_Location.MapToDTO(OLocation);

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }

                return DtoLocation;
            }
        }
    }
}
