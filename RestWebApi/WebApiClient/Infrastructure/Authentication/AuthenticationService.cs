using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebApiClient.Models;

namespace WebApiClient.Infrastructure.Authentication
{
    public class AuthenticationService
    {
        private static UserRepository userRepository = new UserRepository();

        public static bool Authenticate(LoginModel model, HttpResponseBase response)
        {
            var user = /*userRepository.GetUser(model.Email).Result*/new User() { Id = 1, Email = "myemail@email.com", Password = "password" };

            if (user == null)
            {
                return false;
            }

            UserPrincipalSerialized serializeModel = new UserPrincipalSerialized()
            {
                Email = user.Email,
                Id = user.Id
            };

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     serializeModel.Email,
                     DateTime.Now,
                     DateTime.Now.AddDays(1),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            response.Cookies.Add(faCookie);

            return true;
        }
    }
}