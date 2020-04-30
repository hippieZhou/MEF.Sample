using BlankApp.Doamin.Entities;
using System.Linq;

namespace BlackApp.Application.Context
{
    public interface IApplicationDbContext
    {
        IQueryable<TEntity> DbSet<TEntity>() where TEntity : AuditableEntity;
    }
}
