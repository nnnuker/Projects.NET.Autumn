using System.Data.Entity;
using System.Threading.Tasks;

namespace WebApiClient.Infrastructure.Authentication
{
    public class UserRepository
    {
        private static readonly UsersContext usersContext = new UsersContext();

        public async Task<User> GetUser(string email)
        {
            return await usersContext.Users.FirstAsync(user => user.Email == email);
        }
    }
}