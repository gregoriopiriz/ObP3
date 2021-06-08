using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class M_Photo
    {
        public static DTO_Photo MapToDTO(Photo entity)
        {
            DTO_Photo dto = new DTO_Photo();
            dto.ID = entity.ID;
            dto.Photo = entity.Photo1;
            dto.ProductCode = entity.ProductCode;

            return dto;
        }

        public static Photo MapToEntity(DTO_Photo dto)
        {
            Photo entity = new Photo();
            entity.ID = dto.ID;
            entity.Photo1 = dto.Photo;
            entity.ProductCode = dto.ProductCode;

            return entity;
        }
    }
}
