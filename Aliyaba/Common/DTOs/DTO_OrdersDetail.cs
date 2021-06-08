using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class DTO_OrdersDetail
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public string ProductCode { get; set; }
        public int? OrderID { get; set; }

    }
}
