using BlankApp.Doamin.Context;
using BlankApp.Infrastructure.Identity.Entities;
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
        public ApplicationIdentityDbContext(ILoggerFacade loggerFacade) : base(loggerFacade)
        {
        }
    }
}
