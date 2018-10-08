using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebHotel.Models
{
    public class Room
    {
        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }


        [Required]
        [Display(Name = "Level")]
        public string Level { get; set; }

        [Required]
        [Display(Name = "BedCount")]
        public int BedCount { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required] 
        [Display(Name = "TheBookings")]
        public ICollection<Room> TheBookings { get; set; }
    }
}
