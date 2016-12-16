using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiClient.Infrastructure.Authentication
{
    public class UserPrincipalSerialized
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}