using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class DTO_Stock
    {
        public int ID { get; set; }
        public string Location { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public string ProductCode { get; set; }
    }
}
