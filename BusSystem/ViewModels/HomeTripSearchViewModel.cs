using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.ViewModels
{
    public class HomeTripSearchViewModel
    {

        [Required]
        //[NotEqualTo("toId",ErrorMessage ="Drop-off station should be different from Pick-up station!")]
        public int fromId { get; set; }
        
        [Required]
        public int toId  { get; set; }
        
        [Required]
        //[GreaterThanOrEqualTo("CurrentDate")]
        public DateTime departureDate { get; set; }

        [Range(1, 5)]
        [Required]       
        public int passengers { get; set; }

        public DateTime CurrentDate { get; set; } = DateTime.Now;

    }
}
