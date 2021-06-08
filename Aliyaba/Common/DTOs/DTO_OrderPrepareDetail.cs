using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class DTO_OrderPrepareDetail
    {
        public string Location { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}
