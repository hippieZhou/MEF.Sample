namespace BlankApp.Infrastructure.Identity
{
    public interface IIdentityManager
    {
        object CurrentUser { get; }
        bool Login(string userName, string password);
    }
}
