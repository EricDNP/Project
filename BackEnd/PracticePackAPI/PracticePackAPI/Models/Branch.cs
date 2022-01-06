namespace PracticePackAPI.Models
{
    public class Branch : AddressContainer, iEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
    }
}