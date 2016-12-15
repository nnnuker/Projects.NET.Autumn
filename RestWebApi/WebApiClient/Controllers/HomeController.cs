using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebApiClient.Infrastructure.Authentication;
using WebApiClient.Models;

namespace WebApiClient.Controllers
{
    public class HomeController : Controller
    {
        private static UserRepository userRepository = new UserRepository();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var user = userRepository.GetUser(model.Email).Result;

            if (user == null)
            {
                return View();
            }

            UserPrincipal serializeModel = new UserPrincipal(user.Email)
            {
                Id = user.Id
            };

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     model.Email,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(15),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            var httpCookie = HttpContext.Response.Cookies[".ASPXAUTH"];
            if (httpCookie != null)
            {
                httpCookie.Value = null;
            }
            return RedirectToAction("Login");
        }
    }
}
