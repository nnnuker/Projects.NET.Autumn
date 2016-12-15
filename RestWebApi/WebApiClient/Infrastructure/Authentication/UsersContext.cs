using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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