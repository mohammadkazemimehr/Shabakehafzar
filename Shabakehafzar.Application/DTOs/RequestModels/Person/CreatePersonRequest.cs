namespace Shabakehafzar.Application.DTOs.RequestModels.Person
{
    public class CreatePersonRequest
    {
        public string FullName { get; set; }
        public List<CreatePersonAddressRequest> Addresses { get; set; }
    }

    public class CreatePersonAddressRequest
    {
        public string Street { get; set; }
        public string City { get; set; }
    }
}
