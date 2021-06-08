using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class M_OrdersDetail
    {
        public static DTO_OrdersDetail MapToDTO(OrdersDetail entity)
        {
            DTO_OrdersDetail dto = new DTO_OrdersDetail();
            dto.ID = entity.ID;
            dto.Quantity = entity.Quantity;
            dto.ProductCode = entity.ProductCode;
            dto.OrderID = entity.OrderID;

            return dto;
        }

        public static OrdersDetail MapToEntity(DTO_OrdersDetail dto)
        {
            OrdersDetail entity = new OrdersDetail();
            entity.ID = dto.ID;
            entity.Quantity = dto.Quantity;
            entity.ProductCode = dto.ProductCode;
            entity.OrderID = dto.OrderID;

            return entity;
        }
    }
}
