using Microsoft.EntityFrameworkCore;
using Shabakehafzar.Core.Models;

namespace Shabakehafzar.Data.Context
{
    public interface IAppDataContext
    {
        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
