﻿using BlankApp.Doamin.Context;
using BlankApp.Doamin.Entities;
using BlankApp.Infrastructure.Settings;
using Prism.Logging;
using System;
using System.Linq;

namespace BlankApp.Infrastructure.Context
{
    /// <summary>
    /// 数据库上下文,参考使用 EF
    /// http://www.codeisbug.com/Doc/8
    /// https://github.com/sunkaixuan/SqlSugar
    /// </summary>
    public class ApplicationDbContext : IApplicationDbContext
    {
        private readonly DatabaseSettings _databaseSettings;
        protected readonly ILoggerFacade _loggerFacade;

        public IQueryable<Person> Persons { get; set; }
        public ApplicationDbContext(DatabaseSettings databaseSettings, ILoggerFacade loggerFacade)
        {
            _databaseSettings = databaseSettings ?? throw new ArgumentNullException(nameof(databaseSettings));
            _loggerFacade = loggerFacade ?? throw new ArgumentNullException(nameof(loggerFacade));
        }

        public virtual IQueryable<TEntity> DbSet<TEntity>() where TEntity : AuditableEntity
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
