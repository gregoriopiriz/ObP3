using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class M_Stock
    {
        public static DTO_Stock MapToDTO(Stock entity)
        {
            DTO_Stock dto = new DTO_Stock();
            dto.ID = entity.ID;
            dto.Location = entity.Location;
            dto.Reason = entity.Reason;
            dto.Date = entity.Date;
            dto.Quantity = entity.Quantity;
            dto.ProductCode = entity.ProductCode;

            return dto;
        }

        public static Stock MapToEntity(DTO_Stock dto)
        {
            Stock entity = new Stock();
            entity.ID = dto.ID;
            entity.Location = dto.Location;
            entity.Reason = dto.Reason;
            entity.Date = dto.Date;
            entity.Quantity = dto.Quantity;
            entity.ProductCode = dto.ProductCode;

            return entity;
        }
    }
}
