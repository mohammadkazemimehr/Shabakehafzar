using Microsoft.EntityFrameworkCore;
using Shabakehafzar.Core.Data;
using Shabakehafzar.Core.Models;
using Shabakehafzar.Data.Context;
using Shabakehafzar.Data.Exceptions;
using System.Linq.Expressions;

namespace Shabakehafzar.Data.Repository
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields
        private readonly AppDataContext _context;
        private DbSet<T> _entities;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EFRepository(AppDataContext context)
        {
            _context = context;
        }

        #endregion

        public void Delete(Guid id)
        {
            var entity = Entities.FirstOrDefault(e => e.Id == id);
            if (entity == null)
                throw new Exception("Entity with Id " + id.ToString() + " not found");
            Entities.Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                var dbEntity = Entities.FirstOrDefault(e => e.Id == entity.Id);
                if (entity == null)
                    throw new Exception("Entity with Id " + entity.Id.ToString() + " not found");
                Entities.Remove(entity);
            }
        }

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Add(entity);
        }
        public T InsertWithReturn(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            return Entities.Add(entity).Entity;

        }
        public void Insert(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            var dbEntity = Entities.AsNoTracking().FirstOrDefault(e => e.Id == entity.Id);
            entity.IsActive = dbEntity.IsActive;
            entity.CreatedDate = dbEntity.CreatedDate;
            Entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            return await dbQuery.Where(where).AnyAsync();
        }

        public void Clear()
        {
            Entities.RemoveRange(Entities);
        }
    }
}
