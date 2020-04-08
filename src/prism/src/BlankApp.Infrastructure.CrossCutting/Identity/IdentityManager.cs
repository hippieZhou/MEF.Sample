using BlankApp.Infrastructure.Identity;
using System;

namespace BlankApp.Infrastructure.CrossCutting.Identity
{
    public class IdentityManager : IIdentityManager
    {
        public object CurrentUser => throw new NotImplementedException();

        public bool Login(string userName, string password)
        {
            return true;
        }
    }
}
