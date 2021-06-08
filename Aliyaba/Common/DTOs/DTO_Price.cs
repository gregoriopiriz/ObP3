using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class DTO_Price
    {
        public int ID { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string ProductCode { get; set; }
    }
}
