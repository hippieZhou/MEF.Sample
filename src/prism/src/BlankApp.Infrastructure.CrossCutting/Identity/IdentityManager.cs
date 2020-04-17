using BlankApp.Infrastructure.Context;
using BlankApp.Infrastructure.Identity;
using BlankApp.Infrastructure.Identity.Entities;
using System;
using System.Linq;

namespace BlankApp.Infrastructure.CrossCutting.Identity
{
    /// <summary>
    /// 身份认证的具体实现，主要涉及到全局用户的相关操作，比如认证与授权操作
    /// </summary>
    public class IdentityManager : IIdentityManager
    {
        private readonly ApplicationIdentityDbContext _identityDbContext;

        private ApplicationUser _user;
        public ApplicationUser CurrentUser => _user;
        public IdentityManager(ApplicationIdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext ?? throw new ArgumentNullException(nameof(identityDbContext));
        }

        public bool Login(string userName, string password)
        {
            _user = _identityDbContext.Users.FirstOrDefault(x => x.UserName == userName && x.Password == password);
            return _user != null;
        }
    }
}
