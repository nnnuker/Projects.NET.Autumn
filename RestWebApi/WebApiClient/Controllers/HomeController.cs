using System.Web.Mvc;
using WebApiClient.Infrastructure.Authentication;
using WebApiClient.Models;

namespace WebApiClient.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
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
            if (AuthenticationService.Authenticate(model, Response))
            {
                return RedirectToAction("Index");
            }

            return View();            
        }
    }
}
