using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PracticePackFront.Models
{
    public class Card
    {
        public Guid Id { get; set; }

        [Required, DisplayName("Card Holder")]
        public string CardHolder { get; set; }

        [Required, CreditCard]
        public string Number { get; set; }

        [Required, DisplayName("Expiration Date")]
        [BindProperty, DataType("month")]
        [DisplayFormat(DataFormatString = "{0:MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime Expiration { get; set; }

        [Required]
        [RegularExpression(@"^[\d]{3,3}$", ErrorMessage = "Invalid CVV")]
        public int CVV { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
