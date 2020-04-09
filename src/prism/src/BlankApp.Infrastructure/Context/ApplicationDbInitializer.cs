using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using BlankApp.Doamin.Entities;
using System.Linq;

namespace BlankApp.Infrastructure.Context
{
    public static class ApplicationDbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

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

            context.Person = persons.AsQueryable();

            await Task.Yield();
        }
    }
}
