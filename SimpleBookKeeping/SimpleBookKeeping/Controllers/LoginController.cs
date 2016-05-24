using System;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Models;
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

        public ActionResult Authorize(string login, string password)
        {
            if (!(string.IsNullOrEmpty(login) && string.IsNullOrEmpty(password)))
            {
                var auth = MvcApp.Kernel.Get<IAuthentication>();
                auth.HttpContext = System.Web.HttpContext.Current;
                var user = auth.Login(login, password, true);

                if (user == null)
                {
                    ViewData["Error"] = "Login or password incorrect";
                    //ViewBag.Error = "Login or password incorrect";
                }
            }

            return View("index");
        }

    }
}