using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class M_Product
    {
        public static DTO_Product MapToDTO(Product entity)
        {
            DTO_Product dto = new DTO_Product();
            dto.Code = entity.Code;
            dto.Description = entity.Description;
            dto.CategoryID = entity.CategoryID;
            dto.Name = entity.Name;

            return dto;
        }

        public static Product MapToEntity(DTO_Product dto)
        {
            Product entity = new Product();
            entity.Code = dto.Code;
            entity.Description = dto.Description;
            entity.CategoryID = dto.CategoryID;
            entity.Name = dto.Name;

            return entity;
        }
    }
}
