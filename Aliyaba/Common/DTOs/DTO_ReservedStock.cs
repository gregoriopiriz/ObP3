using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class DTO_ReservedStock
    {
        public int ID { get; set; }
        public int? OrderID { get; set; }
        public int? StockID { get; set; }
        public int Quantity { get; set; }
    }
}
