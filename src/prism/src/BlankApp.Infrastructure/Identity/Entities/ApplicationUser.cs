namespace BlankApp.Infrastructure.Identity.Entities
{
    public class ApplicationUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
