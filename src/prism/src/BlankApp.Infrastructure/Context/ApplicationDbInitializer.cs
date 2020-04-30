using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using BlankApp.Doamin.Entities;
using System.Linq;
using BlankApp.Infrastructure.Identity.Entities;
using BlackApp.Application.Context;

namespace BlankApp.Infrastructure.Context
{
    public static class ApplicationDbInitializer
    {
        public static async Task SeedIdentityAsync(ApplicationIdentityDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var users = new List<ApplicationUser>
            {
                new ApplicationUser{ UserName = "管理员", Password = "admin",Role = ApplicationRole.Administrator },
                new ApplicationUser{ UserName = "普通用户",Password = "user",Role = ApplicationRole.User }
            };
            context.Users = users.AsQueryable();
            await Task.Yield();
        }

        public static async Task SeedAsync(IApplicationDbContext context)
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

            await Task.Yield();
        }
    }
}
