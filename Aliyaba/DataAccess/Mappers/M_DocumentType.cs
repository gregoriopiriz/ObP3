using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class M_DocumentType
    {
        public static DTO_DocumentType MapToDTO(DocumentType entity)
        {
            DTO_DocumentType dto = new DTO_DocumentType();
            dto.Name = entity.Name;

            return dto;
        }

        public static DocumentType MapToEntity(DTO_DocumentType dto)
        {
            DocumentType entity = new DocumentType();
            entity.Name = dto.Name;
            return entity;
        }
    }
}
