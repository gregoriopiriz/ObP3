using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class DTO_Delivery
    {
        public int ID { get; set; }

        public DateTime CommitDate { get; set; }

        public string VehiclePlate { get; set; }

        public int? EmployeeID { get; set; } //FK

    }
}
