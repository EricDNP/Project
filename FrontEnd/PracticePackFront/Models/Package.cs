using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PracticePackFront.Models
{
    public class Package
    {
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public float Weight { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required, DisplayName("Departure Date")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        [Required, DisplayName("Arrival Date")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }

        public string Status { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
