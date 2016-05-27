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

            //var session = Db.Session();
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

        public ActionResult Authorize(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (!(string.IsNullOrEmpty(model.Login) && string.IsNullOrEmpty(model.Password)))
                {
                    var auth = MvcApp.Kernel.Get<IAuthentication>();
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