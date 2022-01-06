using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PracticePackAPI.Models
{
    public class AddressContainer
    {
        public string Line2 { get; set; }
        public string Line1 { get; set; }
        public string Sector { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public int PostalCode { get; set; }
    }

    public class Address : AddressContainer, iEntity
    {
        public Guid Id { get; set; }
        public string Label { get; set; }

        [JsonIgnore]
        [NotMapped]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}