namespace Shabakehafzar.Core.Models
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}
