using Microsoft.EntityFrameworkCore;
using Shabakehafzar.Data.Context;
using Shabakehafzar.Data.Repository;

namespace Shabakehafzar.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDataContext _context;
        private IPersonRepository _personRepository;
        public UnitOfWork(AppDataContext context)
        {

            _context = context;

        }
        public IPersonRepository PersonRepository => _personRepository ??= new PersonRepository(_context);
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
