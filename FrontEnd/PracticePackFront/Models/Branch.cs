namespace PracticePackFront.Models
{
    public class Branch : AddressContainer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
    }
}
