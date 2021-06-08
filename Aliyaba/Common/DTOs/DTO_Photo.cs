using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class DTO_Photo
    {
        public int ID { get; set; }
        public byte[] Photo { get; set; }
        public string ProductCode { get; set; }
    }
}
