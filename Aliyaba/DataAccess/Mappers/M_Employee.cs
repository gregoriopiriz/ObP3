using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class M_Employee
    {
        public static DTO_Employee MapToDTO(Employee entity)
        {
            DTO_Employee dto = new DTO_Employee();
            dto.ID = entity.ID;
            dto.UserName = entity.UserName;
            dto.Password = entity.Password;
            dto.Name = entity.Name;
            dto.Surname = entity.Surname;
            dto.positionName = entity.PositionName;

            return dto;
        }

        public static Employee MapToEntity(DTO_Employee dto)
        {
            Employee entity = new Employee();
            entity.ID = dto.ID;
            entity.UserName = dto.UserName;
            entity.Password = dto.Password;
            entity.Name = dto.Name;
            entity.Surname = dto.Surname;
            entity.PositionName = dto.positionName;

            return entity;
        }
    }
}
