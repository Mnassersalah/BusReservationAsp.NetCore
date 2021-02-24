using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Models
{
    public class Ticket
    {
        public int ID { get; set; }

        [Required]
        [Range(1, 5)]
        public int PassengerCount { get; set; }
        
        [Required]
        public string BookedSeats { get; set; }

        [Required]
        [ForeignKey("Trip")]
        public int TripID { get; set; }
        
        //public Client Client { get; set; }

        //Navigation Properties
        public virtual Trip Trip { get; set; }
    }
}
