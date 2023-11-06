namespace Shabakehafzar.Core.Models
{
    public class PersonWithOwnedEntityType : BaseEntity
    {
        public string FullName { get; set; }
        public ICollection<AddressWithOwnedEntityType> Addresses { get; set; }
    }
}
