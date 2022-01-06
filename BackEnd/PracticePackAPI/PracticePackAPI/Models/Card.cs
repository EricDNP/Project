using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PracticePackAPI.Models
{
    public class Card : iEntity
    {
        public Guid Id { get; set; }
        public string CardHolder { get; set; }
        public string Number { get; set; }
        public DateTime Expiration { get; set; }
        public int CVV { get; set; }

        [JsonIgnore][NotMapped]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}