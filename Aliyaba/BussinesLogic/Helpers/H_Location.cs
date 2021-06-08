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
    public class H_Location
    {
        private static H_Location _instance;
        public static H_Location getInstance()
        {
            if (_instance == null)
            {
                _instance = new H_Location();
            }

            return _instance;
        }

        public bool IsValidData(DTO_Location _L)
        {
            if (_L.Latitude != 0 && _L.Longitude != 0)
            {
                try
                {
                    using (AliyabaEntities context = new AliyabaEntities())
                    {
                        if (context.Locations.Any(a => a.CustomerUsername == _L.CustomerUsername) && context.Locations.Any(a => a.Latitude == _L.Latitude) && context.Locations.Any(a => a.Longitude == _L.Longitude))
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

        public void AddLocation(DTO_Location _L)
        {
            if (IsValidData(_L))
            {
                P_Location.AddLocation(_L);
            }
        }

        public void DeleteLocation(int ID)
        {
            P_Location.DeleteLocation(ID);
        }

        public List<DTO_Location> GetLocations()
        {
            return P_Location.GetAllLocations();
        }

        public List<DTO_Location> GetLocationsByUser(string UserName)
        {
            return P_Location.GetLocationsByUser(UserName);
        }
        public List<DTO_Location> GetLocationsByDeliveryID(int _DeliveryID)
        {
            return P_Location.GetLocationsByDeliveryID(_DeliveryID);
        }
        public DTO_Location GetLocationByID(int ID)
        {
            return P_Location.GetLocationByID(ID);
        }
    }
}
