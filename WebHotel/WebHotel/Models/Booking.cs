using System.ComponentModel.DataAnnotations;

namespace WebHotel.Models
{
    public class Booking
    {
        [Key]
        public int ID { get; set; }

        //Foreign Key
        public int RoomID { get; set; }

        //Foreign Key
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Check In")]
        public string CheckIn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Check Out")]
        public string CheckOut { get; set; }

        [Required]
        [Display(Name = "Cost")]
        public decimal Cost { get; set; }

        // Navigation properties
        public Room TheRoom { get; set; }
        public Customer TheCustomer { get; set; }
    }
}