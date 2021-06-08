using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class M_Customer
    {
        public static DTO_Customer MapToDTO(Customer entity)
        {
            DTO_Customer dto = new DTO_Customer();
            dto.UserName = entity.UserName;
            dto.Password = entity.Password;
            dto.Name = entity.Name;
            dto.Surname = entity.Surname;
            dto.Document = entity.Document;
            dto.Email = entity.Email;
            dto.PhoneNumber = entity.PhoneNumber;
            dto.DocumentTypeName = entity.DocumentTypeName;

            return dto;
        }

        public static Customer MapToEntity(DTO_Customer dto)
        {
            Customer entity = new Customer();
            entity.UserName = dto.UserName;
            entity.Password = dto.Password;
            entity.Name = dto.Name;
            entity.Surname = dto.Surname;
            entity.Document = dto.Document;
            entity.Email = dto.Email;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.DocumentTypeName = dto.DocumentTypeName;

            return entity;
        }
    }
}
