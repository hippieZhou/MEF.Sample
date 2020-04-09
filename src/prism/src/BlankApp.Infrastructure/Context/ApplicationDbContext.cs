using BlankApp.Doamin.Entities;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlankApp.Infrastructure.Context
{
    /// <summary>
    /// 数据库上下文,参考使用 EF
    /// </summary>
    public class ApplicationDbContext
    {
        private readonly ILoggerFacade _loggerFacade;
        public IQueryable<Person> Person { get; set; }

        public ApplicationDbContext(ILoggerFacade loggerFacade)
        {
            _loggerFacade = loggerFacade ?? throw new ArgumentNullException(nameof(loggerFacade));
        }

        public IQueryable<TEntity> DbSet<TEntity>() where TEntity : AuditableEntity
        {
            IQueryable<TEntity> entities = default;

            var properties = GetType().GetProperties();
            foreach (var prop in properties)
            {
                var genericType = prop.PropertyType.GenericTypeArguments.FirstOrDefault();
                _loggerFacade.Log($"{genericType}:{typeof(TEntity)}", Category.Debug, Priority.None);
                if (genericType == typeof(TEntity))
                {
                    entities = prop.GetValue(this) as IQueryable<TEntity>;
                }
            }

            return entities;
        }
    }
}
