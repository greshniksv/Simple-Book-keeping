using System.Web.Mvc;

namespace SimpleBookKeeping.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }
    }
}