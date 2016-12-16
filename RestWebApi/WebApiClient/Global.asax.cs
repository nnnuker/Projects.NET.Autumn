using System;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebApiClient.Infrastructure.Authentication;

namespace WebApiClient
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null && authCookie.Value != "")
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                UserPrincipalSerialized serializeModel = serializer.Deserialize<UserPrincipalSerialized>(authTicket.UserData);

                UserPrincipal newUser = new UserPrincipal(authTicket.Name)
                {
                    Email = serializeModel.Email,
                    Id = serializeModel.Id
                };

                HttpContext.Current.User = newUser;
            }
        }
    }
}
