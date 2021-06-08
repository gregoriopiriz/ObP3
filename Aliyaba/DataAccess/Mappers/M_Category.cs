using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Common.DTOs;

namespace DataAccess.Mappers
{
    public class M_Category
    {
        public static DTO_Category MapToDTO(Category C)
        {
            DTO_Category DTO = new DTO_Category();
            DTO.ID = C.ID;
            DTO.Name = C.Name;

            return DTO;
        }
        public static Category MapToEntity(DTO_Category DTO)
        {
            Category C = new Category();
            C.ID = DTO.ID;
            C.Name = DTO.Name;

            return C;
        }
    }
}
