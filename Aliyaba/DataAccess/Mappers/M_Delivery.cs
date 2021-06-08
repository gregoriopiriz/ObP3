using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class M_Delivery
    {
        public static DTO_Delivery MapToDTO(Delivery entity)
        {
            DTO_Delivery dto = new DTO_Delivery();
            dto.ID = entity.ID;
            dto.CommitDate = entity.CommitDate;
            dto.VehiclePlate = entity.VehiclePlate;
            dto.EmployeeID = entity.EmployeeID;

            return dto;
        }

        public static Delivery MapToEntity(DTO_Delivery dto)
        {
            Delivery entity = new Delivery();
            entity.ID = dto.ID;
            entity.CommitDate = dto.CommitDate;
            entity.VehiclePlate = dto.VehiclePlate;
            entity.EmployeeID = dto.EmployeeID;

            return entity;
        }
    }
}
