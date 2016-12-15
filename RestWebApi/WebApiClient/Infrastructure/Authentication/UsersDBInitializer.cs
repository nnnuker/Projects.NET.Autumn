using System.Data.Entity;

namespace WebApiClient.Infrastructure.Authentication
{
    public class UsersDBInitializer: CreateDatabaseIfNotExists<UsersContext>
    {
        protected override void Seed(UsersContext context)
        {
            context.Users.Add(new User() {Id = 1, Email = "myemail@email.com", Password = "password"});
        }
    }
}