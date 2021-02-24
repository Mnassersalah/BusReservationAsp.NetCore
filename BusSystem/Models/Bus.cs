using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Models
{
    public class Bus
    {
        public int ID { get; set; }

        [Required]
        [RegularExpression("[A-Z]{3}[0-9]{3}")]
        public string BusNum { get; set; }

        [Required]
        [EnumDataType(typeof(Category))]
        public Category Category { get; set; }

        [Required]
        [Range(1,60)]
        public int Capacity{ get; set; }

        //Navigation Properities
        public virtual List<Trip> Trips { get; set; }
    }
}
