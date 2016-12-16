using System.Data.Entity;

namespace WebApiClient.Infrastructure.Authentication
{
    public class UsersContext : DbContext
    {
        public UsersContext() : base("name=UsersContext")
        {
            Database.SetInitializer(new UsersDBInitializer());
        }

        public DbSet<User> Users { get; set; }
    }
}