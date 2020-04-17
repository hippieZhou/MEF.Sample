using BlankApp.Doamin.Context;
using BlankApp.Infrastructure.Identity.Entities;
using BlankApp.Infrastructure.Settings;
using Prism.Logging;
using System.Linq;

namespace BlankApp.Infrastructure.Context
{
    /// <summary>
    /// 身份认证的类
    /// </summary>
    public class ApplicationIdentityDbContext : ApplicationDbContext, IApplicationDbContext
    {
        public IQueryable<ApplicationUser> Users { get; set; }
        public ApplicationIdentityDbContext(DatabaseSettings databaseSettings, ILoggerFacade loggerFacade) : base(databaseSettings, loggerFacade)
        {
        }
    }
}
