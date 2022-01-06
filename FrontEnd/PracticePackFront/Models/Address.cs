using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PracticePackFront.Models
{
    public class AddressContainer
    {
        [Required, DisplayName("Address 1")]
        public string Line1 { get; set; }
        [DisplayName("Addres 2")]
        public string Line2 { get; set; }
        [Required]
        public string Sector { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }
        [Required, DisplayName("Postal Code")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Postal Code")]
        public int PostalCode { get; set; }
    }

    public class Address : AddressContainer
    {
        public Guid Id { get; set; }
        [Required]
        public string Label { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
