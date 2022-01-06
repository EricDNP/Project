using System.ComponentModel.DataAnnotations.Schema;

namespace PracticePackAPI.Models
{
    public class User : iEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Cedula { get; set; }
        public string Telephone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        
        [NotMapped] [ForeignKey(nameof(BranchId))]
        public Branch Branch { get; set; }
        public Guid? BranchId { get; set; }
        public ICollection<Address> Adresses { get; set; }
        public ICollection<Card> Cards { get; set; }
        public ICollection<Package> Packages { get; set; }
    }
}