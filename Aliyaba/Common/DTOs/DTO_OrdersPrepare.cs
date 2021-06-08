using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class DTO_OrdersPrepare
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public int? OrderID { get; set; } //FK

        public int? DeliveryID { get; set; } //FK

        public int? EmployeeID { get; set; } //FK
    }
}
