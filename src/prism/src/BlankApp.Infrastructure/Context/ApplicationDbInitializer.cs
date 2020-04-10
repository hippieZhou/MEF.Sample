using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using BlankApp.Doamin.Entities;
using System.Linq;
using BlankApp.Infrastructure.Identity.Entities;

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

            var users = new List<ApplicationUser>
            {
                new ApplicationUser{ UserName = "管理员",Role = ApplicationRole.Administrator },
                new ApplicationUser{ UserName = "普通用户",Role = ApplicationRole.User }
            };
            context.Users = users.AsQueryable();

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
