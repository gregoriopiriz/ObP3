using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    class M_Location
    {
        public static DTO_Location MapToDTO(Location entity)
        {
            DTO_Location dto = new DTO_Location();
            dto.ID = entity.ID;
            dto.Description = entity.Description;
            dto.Latitude = entity.Latitude;
            dto.Longitude = entity.Longitude;
            dto.CustomerUsername = entity.CustomerUsername;

            return dto;
        }

        public static Location MapToEntity(DTO_Location dto)
        {
            Location entity = new Location();
            entity.ID = dto.ID;
            entity.Description = dto.Description;
            entity.Latitude = dto.Latitude;
            entity.Longitude = dto.Longitude;
            entity.CustomerUsername = dto.CustomerUsername;

            return entity;
        }
    }
}
