namespace BlankApp.Infrastructure.Settings
{
    public class DatabaseSettings
    {
        public string DefaultConnection { get; protected set; }
        public int TimeoutSeconds { get; protected set; }
    }
}
