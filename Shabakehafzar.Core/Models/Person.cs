namespace Shabakehafzar.Core.Models
{
    public class Person : BaseEntity
    {
        public Person()
        {
            Addresses = new List<Address>();
        }
        public string FullName { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }

}
