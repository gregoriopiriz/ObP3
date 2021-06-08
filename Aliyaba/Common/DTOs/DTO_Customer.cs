using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class DTO_Customer
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DocumentTypeName { get; set; }
    }
}
