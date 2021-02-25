using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Models
{
    public class Trip
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Departure Date")]
        [DataType(DataType.DateTime)]
        public DateTime StartDateTime { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        [Display(Name = "Available Seats")]
        public string AvailableSeats { get; set; }

        [Required]
        [ForeignKey("Route")]
        public int RouteID { get; set; }

        [Required]
        [ForeignKey("Bus")]
        public int BusID { get; set; }

        //Navigation Properties
        public Route Route { get; set; }
        public virtual Bus Bus { get; set; }
        public virtual List<Ticket> Tickets { get; set; }

        public override string ToString()
        {
            return $"{ID} {Route} {StartDateTime}";
        }

        [NotMapped]
        public string tostringprop { get => $"{ID} {Route} {StartDateTime}"; }

        [NotMapped]
        public string AvailableSeatsCount
        {
            get
            {
                if (AvailableSeats != null)
                    return $"{AvailableSeats.Split(",").Count()}";
                else
                    return AvailableSeats;
            }
        }

        [NotMapped]
        public string RouteToString
        {
            get
            {
                if (Route != null)
                    return Route.ToString();
                else
                    return null;
            }
        }
    }
}
