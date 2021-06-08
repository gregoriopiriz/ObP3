using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class DTO_Order
    {
        public int ID { get; set; }
        public int? DeliveryID { get; set; }

        public bool IsExpress { get; set; }

        public double TotalPrice { get; set; }

        public DateTime EntryDate { get; set; }

        public int? LocationID { get; set; } //FK

        public int CurrentStateID { get; set; }

        public List<DTO_State> States { get; set; }

        public string CustomerUsername { get; set; }

        public string TimeLeft {get;set;}
    }
}
