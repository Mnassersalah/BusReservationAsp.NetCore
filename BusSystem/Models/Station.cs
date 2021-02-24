using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Models
{
    public class Station
    {
        public int ID { get; set; }

        [Required]
        public string City { get; set; }
        public string Name { get; set; }

        [InverseProperty("PickUp")]
        public virtual HashSet<Route> PickUpRoutes { get; set; } 

        [InverseProperty("DropOff")]
        public virtual HashSet<Route> DropOffRoutes { get; set; } 

        public override string ToString()
        {
            return Name==null? City: $"{City} ( {Name} )";
        }
    }
}
