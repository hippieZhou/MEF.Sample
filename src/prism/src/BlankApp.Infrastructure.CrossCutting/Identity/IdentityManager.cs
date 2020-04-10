using BlankApp.Infrastructure.Identity;
using BlankApp.Infrastructure.Identity.Entities;
using System;

namespace BlankApp.Infrastructure.CrossCutting.Identity
{
    /// <summary>
    /// 身份认证的具体实现，主要涉及到全局用户的相关操作，比如认证与授权操作
    /// </summary>
    public class IdentityManager : IIdentityManager
    {
        private ApplicationUser _user;
        public ApplicationUser CurrentUser => _user;

        public bool Login(ApplicationUser user)
        {
            _user = user;
            return true;
        }
    }
}
