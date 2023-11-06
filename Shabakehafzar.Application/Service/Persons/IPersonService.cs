using Shabakehafzar.Application.DTOs.RequestModels.Person;
using Shabakehafzar.Application.DTOs.ResponceModels.Person;

namespace Shabakehafzar.Application.Service.Persons
{
    public interface IPersonService
    {
        Task<IEnumerable<GetAllPersonResponce>> GetAllWithLinq();
        Task<IEnumerable<GetAllPersonResponce>> GetAllWithLambda();
        Task<IEnumerable<GetAllPersonResponce>> GetAllWithTSQL();
        Task<Guid> Create(CreatePersonRequest command);
    }
}
