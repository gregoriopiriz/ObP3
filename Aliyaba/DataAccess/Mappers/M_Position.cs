using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class M_Position
    {
        public static DTO_Position MapToDTO(Position entity)
        {
            DTO_Position dto = new DTO_Position();
            dto.Name = entity.Name;
            dto.Salary = entity.Salary;

            return dto;
        }

        public static Position MapToEntity(DTO_Position dto)
        {
            Position entity = new Position();
            entity.Name = dto.Name;
            entity.Salary = dto.Salary;

            return entity;
        }
    }
}
