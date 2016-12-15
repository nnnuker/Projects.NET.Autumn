using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebApiClient.Infrastructure.Authentication
{
    public class UserPrincipal : IPrincipal
    {
        public IIdentity Identity { get; }

        public int Id { get; set; }
        public string Email { get; set; }

        public UserPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}