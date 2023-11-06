namespace Shabakehafzar.Application.DTOs.ResponceModels.Person
{
    public class GetAllPersonResponce
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public IEnumerable<PersonAddressResponce> Addresses { get; set; }
    }
    public class PersonAddressResponce
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }
}
