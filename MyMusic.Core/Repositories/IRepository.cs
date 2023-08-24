using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyMusic.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        // Expression<Func<TEntity, bool>> predicate is a lambda expression that takes a TEntity and returns a bool
        // Find returns a list of entities that match the predicate
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        // SingleOrDefalutAsync returns a single entity or a default value if no entity is found
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        // AddAsync adds a new entity to the database
        Task AddAsync(TEntity entity);
        // AddRangeAsync adds a range of entities to the database
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        // Remove removes an entity from the database
        void Remove(TEntity entity);
        // RemoveRange removes a range of entities from the database
        void RemoveRange(IEnumerable<TEntity> entities);

    }
}