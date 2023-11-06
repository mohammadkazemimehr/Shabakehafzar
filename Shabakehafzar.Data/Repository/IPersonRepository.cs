using Shabakehafzar.Core.Data;
using Shabakehafzar.Core.Models;
using Shabakehafzar.Data.DTOs.Person;

namespace Shabakehafzar.Data.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<ICollection<Person>> GetAllWithLinq();
        Task<ICollection<Person>> GetAllWithLambda();
        Task<ICollection<Person>> GetAllWithTSQL();
        Guid Create(string fullName, IEnumerable<CreatePersonAddressDto> personAddressDto);

    }
}
