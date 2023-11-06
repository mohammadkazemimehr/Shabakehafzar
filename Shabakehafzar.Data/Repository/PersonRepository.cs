using Microsoft.EntityFrameworkCore;
using Shabakehafzar.Core.Models;
using Shabakehafzar.Data.Context;
using Shabakehafzar.Data.DTOs.Person;

namespace Shabakehafzar.Data.Repository
{
    public class PersonRepository : EFRepository<Person> , IPersonRepository
    {
        private readonly AppDataContext _context;
        public PersonRepository(AppDataContext context): base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Person>> GetAllWithLambda()
        {
            return await _context.Persons.Include(c => c.Addresses).ToListAsync();
        }

        public async Task<ICollection<Person>> GetAllWithLinq()
        {
            var queryResult = from p in _context.Persons
                         join a in _context.Addresses on p.Id equals a.PersonId
                         select new { p, a };

            var result = await queryResult.ToListAsync();
            return result.Select(c=>c.p).ToList();
        }

        public async Task<ICollection<Person>> GetAllWithTSQL()
        {
            var query = "SELECT * FROM [ShabakehafzarDb].[dbo].[Person]" +
                "\r\nINNER JOIN  Address on Person.Id = Address.PersonId";
            return await _context.Persons.FromSqlRaw(query).ToListAsync();
        }

        public Guid Create(string fullName,IEnumerable<CreatePersonAddressDto> personAddressDto)
        {
            var person = new Person
            {
                FullName = fullName
            };

            person.Addresses = personAddressDto.Select(a => new Address
            {
                City = a.City,
                Street = a.Street,
                Person = person,
                PersonId = person.Id
            }).ToList();

            _context.Persons.Add(person);

            return person.Id;
        }
    }
}
