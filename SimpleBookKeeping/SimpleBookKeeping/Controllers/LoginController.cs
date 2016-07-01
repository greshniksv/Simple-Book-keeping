using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Authorize(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (!(string.IsNullOrEmpty(model.Login) && string.IsNullOrEmpty(model.Password)))
                {
                    var auth = MvcApp.Unity.Resolve<IAuthentication>();
                    auth.HttpContext = System.Web.HttpContext.Current;
                    var user = auth.Login(model.Login, model.Password, true);

                    if (user == null)
                    {
                        ViewBag.Error = "Login or password incorrect";
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View("index", model);
        }

    }
}