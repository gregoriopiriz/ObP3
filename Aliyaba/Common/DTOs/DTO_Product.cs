using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class DTO_Product
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public DTO_Price Price { get; set; }
        public List<DTO_Stock> Stocks { get; set; }
    }
}
