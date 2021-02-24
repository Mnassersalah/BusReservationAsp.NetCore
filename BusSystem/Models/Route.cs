using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Models
{
    public class Route
    {
        public int ID { get; set; }
        

        [Required]
        [Range(1, int.MaxValue)]
        public int Duration { get; set; }

        [Display(Name = "Pick-up Location")]
        [ForeignKey("PickUp")]
        public int? PickUpID { get; set; }

        [Display(Name = "Drop-off Location")]
        [ForeignKey("DropOff")]
        public int? DropOffID { get; set; }

        //Navigation Properities
        public virtual Station PickUp { get; set; }
        public virtual Station DropOff { get; set; }
        public virtual List<Trip> Trips{ get; set; }

        public override string ToString()
        {
            return $"{PickUp} => {DropOff}"; 
        }


    }
}
