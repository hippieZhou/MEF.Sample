using BlankApp.Infrastructure.Identity;
using System;

namespace BlankApp.Infrastructure.CrossCutting.Identity
{
    /// <summary>
    /// 身份认证的具体实现，主要涉及到全局用户的相关操作，比如认证与授权操作
    /// </summary>
    public class IdentityManager : IIdentityManager
    {
        public object CurrentUser => throw new NotImplementedException();

        public bool Login(string userName, string password)
        {
            return true;
        }
    }
}
