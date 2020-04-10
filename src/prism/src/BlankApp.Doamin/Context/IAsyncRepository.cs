using BlankApp.Doamin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlankApp.Doamin.Context
{
    /// <summary>
    /// 抽象仓储的具体实现，主要负责一些数据库表的常规操作
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IAsyncRepository<TEntity> where TEntity : AuditableEntity
    {
        IQueryable<TEntity> Table { get; }
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<TEntity> FindByIdAsync(Guid id);
    }
}
