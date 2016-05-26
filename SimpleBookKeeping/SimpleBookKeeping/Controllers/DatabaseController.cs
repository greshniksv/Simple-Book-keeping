using System;
using System.Web.Mvc;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;

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
               DBHelper.CreateDb();
            }
            catch (Exception ex)
            {
                ViewBag.Response = ex.ToString();
                return View("index");
            }

            ViewBag.Response = "Database created";

            return View("index");
        }
    }
}