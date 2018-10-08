using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebHotel.Models
{
    public class Customer
    {
        [Key]
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Given Name")]
        [RegularExpression(@"[A-Z][a-z'-]{2,20}", ErrorMessage = "Given Name's length between 2 and 20 characters")]
        public string GivenName { get; set; }


        [Required]
        [Display(Name = "Family Name")]
        [RegularExpression(@"[A-Z][a-z'-]{2,20}", ErrorMessage = "Family Name's length between 2 and 20 characters")]
        public string SurName { get; set; }

        [Required]
        [Display(Name = "Postcode")]
        [RegularExpression(@"^[1-8][0-9]{3}$", ErrorMessage = "The Postal Code field is required and consists of exactly 4 digits; and its first digit cannot be '9' for residential use!")]
        public string Postcode { get; set; }

        // Navigation properties
        public ICollection<Booking> TheBookings { get; set; }
    }
}
