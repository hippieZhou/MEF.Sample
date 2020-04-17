using BlankApp.Doamin.Entities;
using System.Linq;

namespace BlankApp.Doamin.Context
{
    public interface IApplicationDbContext
    {
        IQueryable<TEntity> DbSet<TEntity>() where TEntity : AuditableEntity;
    }
}
