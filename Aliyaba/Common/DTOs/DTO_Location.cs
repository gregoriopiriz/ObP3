using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class DTO_Location
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string CustomerUsername { get; set; } //FK
    }
}
