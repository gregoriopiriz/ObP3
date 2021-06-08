using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class M_ReservedStock
    {
        public static DTO_ReservedStock MapToDTO(ReservedStock entity)
        {
            DTO_ReservedStock dto = new DTO_ReservedStock();
            dto.ID = entity.ID;
            dto.StockID = entity.StockID;
            dto.OrderID = entity.OrderID;
            dto.Quantity = entity.Quantity;
            return dto;
        }

        public static ReservedStock MapToEntity(DTO_ReservedStock dto)
        {
            ReservedStock entity = new ReservedStock();
            entity.ID = dto.ID;
            entity.StockID = dto.StockID;
            entity.OrderID = dto.OrderID;
            entity.Quantity = dto.Quantity;

            return entity;
        }
    }
}
