using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class M_Order
    {
        public static DTO_Order MapToDTO(Order entity)
        {
            DTO_Order dto = new DTO_Order();
            dto.ID = entity.ID;
            dto.DeliveryID = entity.DeliveryID;
            dto.IsExpress = entity.IsExpress;
            dto.TotalPrice = entity.TotalPrice;
            dto.EntryDate = entity.EntryDate;
            dto.LocationID = entity.LocationID;
            dto.CustomerUsername = entity.CustomerUsername;

            TimeSpan d = new TimeSpan(24, 0, 0);
            double DaysBetween = (DateTime.Now - entity.EntryDate).TotalDays;

            if (DaysBetween >= 1)
            {
                dto.TimeLeft = "0";
            }
            else
            {
                dto.TimeLeft = (d - (DateTime.Now - entity.EntryDate)).ToString("hh\\.mm");
            }

            return dto;
        }

        public static Order MapToEntity(DTO_Order dto)
        {
            Order entity = new Order();
            entity.ID = dto.ID;
            entity.DeliveryID = dto.DeliveryID;
            entity.IsExpress = dto.IsExpress;
            entity.TotalPrice = dto.TotalPrice;
            entity.EntryDate = dto.EntryDate;
            entity.LocationID = dto.LocationID;
            entity.CustomerUsername = dto.CustomerUsername;

            return entity;
        }
    }
}
