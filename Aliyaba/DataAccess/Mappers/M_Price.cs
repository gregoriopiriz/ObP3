using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class M_Price
    {
        public static DTO_Price MapToDTO(Price entity)
        {
            DTO_Price dto = new DTO_Price();
            dto.ID = entity.ID;
            dto.Price = entity.Price1;
            dto.Date = entity.Date;
            dto.ProductCode = entity.ProductCode;

            return dto;
        }

        public static Price MapToEntity(DTO_Price dto)
        {
            Price entity = new Price();
            entity.ID = dto.ID;
            entity.Price1 = dto.Price;
            entity.Date = dto.Date;
            entity.ProductCode = dto.ProductCode;

            return entity;
        }
    }
}
