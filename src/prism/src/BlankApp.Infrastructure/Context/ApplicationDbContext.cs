using BlankApp.Doamin.Entities;
using BlankApp.Infrastructure.Identity.Entities;
using BlankApp.Infrastructure.Settings;
using Prism.Logging;
using System;
using System.Linq;

namespace BlankApp.Infrastructure.Context
{
    /// <summary>
    /// 数据库上下文,参考使用 EF
    /// http://www.codeisbug.com/Doc/8
    /// </summary>
    public class ApplicationDbContext
    {
        private readonly DatabaseSettings _connectionStrings;
        private readonly ILoggerFacade _loggerFacade;

        public IQueryable<ApplicationUser> Users { get; set; }
        public IQueryable<Person> Person { get; set; }

        public ApplicationDbContext(DatabaseSettings connectionStrings, ILoggerFacade loggerFacade)
        {
            _connectionStrings = connectionStrings ?? throw new ArgumentNullException(nameof(connectionStrings));
            _loggerFacade = loggerFacade ?? throw new ArgumentNullException(nameof(loggerFacade));
        }

        public IQueryable<TEntity> DbSet<TEntity>() where TEntity : AuditableEntity
        {
            var properties = GetType().GetProperties();
            foreach (var prop in properties)
            {
                var genericType = prop.PropertyType.GenericTypeArguments.FirstOrDefault(x => x == typeof(TEntity));
                _loggerFacade.Log($"{genericType}", Category.Debug, Priority.None);
                if (genericType != null)
                {
                    return prop.GetValue(this) as IQueryable<TEntity>;
                }
            }

            return default;
        }
    }
}
