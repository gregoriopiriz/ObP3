using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public enum States {Pending, Preparing, Prepared, ReadyForDelivery, Delivering, Delivered, Cancelled};
    public class DTO_State
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public States State { get; set; }

        public int OrderID { get; set; }

        public int? EmployeeID { get; set; } //FK
    }
}
