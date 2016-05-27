using System;
using System.Web.Mvc;
using SimpleBookKeeping.Database;

namespace SimpleBookKeeping.Controllers
{
    public class DatabaseController : Controller
    {
        // GET: Database
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateDatabase()
        {
            try
            {
               Db.CreateDb();
            }
            catch (Exception ex)
            {
                return RedirectToAction("CreateResult",
                    new { message = ex.Message, success = false });
            }
            
            return RedirectToAction("CreateResult", 
                new { message = "Database created successful", success = true });
        }
        
        public ActionResult CreateResult(string message, bool success)
        {
            if (success)
            {
                ViewBag.Info = message;
            }
            else
            {
                ViewBag.Error = message;
            }

            return View("Index");
        }

    }
}