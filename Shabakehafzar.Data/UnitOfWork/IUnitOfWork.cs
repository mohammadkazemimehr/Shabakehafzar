using Shabakehafzar.Data.Repository;

namespace Shabakehafzar.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        Task<int> CommitAsync();

    }
}
