using BlankApp.Doamin.Entities;
using BlankApp.Infrastructure.Identity.Entities;
using Prism.Logging;
using SqlSugar;
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
        private readonly ISqlSugarClient _sugarClient;
        private readonly ILoggerFacade _loggerFacade;

        public IQueryable<ApplicationUser> Users { get; set; }
        public IQueryable<Person> Person { get; set; }

        public ApplicationDbContext(SqlSugarClient sugarClient, ILoggerFacade loggerFacade)
        {
            _sugarClient = sugarClient;
            _loggerFacade = loggerFacade ?? throw new ArgumentNullException(nameof(loggerFacade));
        }

        private SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "data source= db.sqlite3",
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            db.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(Person));

            //Print sql
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                var message = sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value));
                _loggerFacade.Log(message, Category.Debug, Priority.None);
            };
            return db;
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
