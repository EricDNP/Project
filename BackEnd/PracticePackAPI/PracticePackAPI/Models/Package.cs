using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PracticePackAPI.Models
{
    public class Package : iEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
        public decimal Price { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Status { get; set; }

        [JsonIgnore][NotMapped]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}