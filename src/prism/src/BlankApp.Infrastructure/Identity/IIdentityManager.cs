using BlankApp.Infrastructure.Identity.Entities;

namespace BlankApp.Infrastructure.Identity
{
    public interface IIdentityManager
    {
        ApplicationUser CurrentUser { get; }
        bool Login(ApplicationUser user);
    }
}
