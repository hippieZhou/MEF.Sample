using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using BlankApp.Doamin.Entities;
using System.Linq;
using BlankApp.Infrastructure.Identity.Entities;
using BlankApp.Doamin.Context;
using BlankApp.Doamin.Framework;

namespace BlankApp.Infrastructure.Context
{
    public static class ApplicationDbInitializer
    {
        public static async Task SeedAsync()
        {
            var identityDbContext = EnginContext.Current.Resolve<ApplicationIdentityDbContext>();
            if (identityDbContext == null)
            {
                throw new ArgumentNullException(nameof(ApplicationIdentityDbContext));
            }

            var users = new List<ApplicationUser>
            {
                new ApplicationUser{ UserName = "管理员", Password = "admin",Role = ApplicationRole.Administrator },
                new ApplicationUser{ UserName = "普通用户",Password = "user",Role = ApplicationRole.User }
            };
            identityDbContext.Users = users.AsQueryable();


            var dbContext = EnginContext.Current.Resolve<ApplicationDbContext>();
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(IApplicationDbContext));
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
            dbContext.Persons = persons.AsQueryable();
            await Task.Yield();
        }
    }
}
