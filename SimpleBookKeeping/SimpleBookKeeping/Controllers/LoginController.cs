using System.Web.Mvc;
using Ninject;
using SimpleBookKeeping.Unility.Interfaces;

namespace SimpleBookKeeping.Controllers
{
    public class LoginController : Controller
    {
        private IHashCalculator hashCalculator;

        // GET: Login
        public ActionResult Index()
        {
            hashCalculator = MvcApplication.AppKernel.Get<IHashCalculator>();

            ViewBag.Hash = hashCalculator.GetHash("Bla bla");

            return View();
        }
    }
}