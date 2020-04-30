using BlackApp.Application.Context;
using BlankApp.Doamin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlankApp.Infrastructure.Context
{
    /// <summary>
    /// 该类请勿直接使用，请通过注入 IAsyncRepository<TEntity> 的方式来使用
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : AuditableEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public AsyncRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IQueryable<TEntity> Table => _dbContext.DbSet<TEntity>();

        public Task AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
