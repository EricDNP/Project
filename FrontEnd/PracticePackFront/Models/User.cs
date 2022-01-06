using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PracticePackFront.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required, DisplayName("Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Mail { get; set; }

        [Range(10000000000, 99999999999,
        ErrorMessage = "Invalid Cedula Number")]
        public string Cedula { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
            ErrorMessage = "Not a valid phone number")]
        public string Telephone { get; set; }

        [Required, StringLength(int.MaxValue, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DisplayName("Confirm Password")]
        [DataType(DataType.Password), StringLength(int.MaxValue, MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }
        public ICollection<Address> Adresses { get; set; }
        public ICollection<Card> Cards { get; set; }
        public ICollection<Package> Packages { get; set; }

        public string Contact
        {
            get { return this.Name + " " + this.LastName; }
        }
    }
}
