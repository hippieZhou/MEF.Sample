using BlankApp.Doamin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlankApp.Infrastructure.Context
{
    /// <summary>
    /// 数据库上下文,参考使用 EF
    /// </summary>
    public class ApplicationDbContext
    {
        public ApplicationDbContext()
        {
            var persons = new List<Person>();
            Enumerable.Range(0, 100).ToList().ForEach(i => 
            {
                var person = new Person
                {
                    Id = Guid.NewGuid(),
                    Name = $"{i}.Nick",
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                };
                persons.Add(person);
            });
            Persons = persons.AsQueryable();
        }

        public IQueryable<TEntity> DbSet<TEntity>() where TEntity : AuditableEntity
        {
            IQueryable<TEntity> entities = default;

            var properties = GetType().GetProperties();
            foreach (var prop in properties)
            {
                var genericType = prop.PropertyType.GenericTypeArguments.FirstOrDefault();
                if (genericType == typeof(TEntity))
                {
                    entities = prop.GetValue(this) as IQueryable<TEntity>;
                }
            }
            return entities;
        }

        public IQueryable<Person> Persons { get; set; } 
    }
}
