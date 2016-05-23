using System.Collections.Generic;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Util;
using Ninject;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Unility.Interfaces;

namespace SimpleBookKeeping.Controllers
{
    public class LoginController : Controller
    {
        private IHashCalculator hashCalculator;

        // GET: Login
        public ActionResult Index()
        {

            //    hashCalculator = MvcApp.Kernel.Get<IHashCalculator>();
            //    ViewBag.Hash = hashCalculator.GetHash("Bla bla");

            //var session = DBHelper.OpenSession();
            //using (ITransaction transaction = session.BeginTransaction())
            //{
            //    ICriteria criteria = session.CreateCriteria(typeof(User));
            //    criteria.Add(Restrictions.Eq("Id", "efd174aa-fd61-40f4-ae37-ea5fe1cc0f6d"));
            //    IList<User> matchingObjects = criteria.List<User>();
            //    transaction.Commit();
            //    var user = matchingObjects.First();
            //}



            return View();
        }

        [HttpGet]
        public ActionResult Login(string user, string password)
        {
            var auth = MvcApp.Kernel.Get<IAuthentication>();
            auth.HttpContext = System.Web.HttpContext.Current;
            auth.Login(user, password, true);

            return View("index");
        }

    }
}