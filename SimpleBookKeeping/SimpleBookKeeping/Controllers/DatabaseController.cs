using System;
using System.Web.Mvc;
using MediatR;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Commands.Clear;
using SimpleBookKeeping.Database;

namespace SimpleBookKeeping.Controllers
{
    [Authorize]
    public class DatabaseController : Controller
    {
        private readonly IMediator _mediator;

        public DatabaseController()
        {
            _mediator = MvcApp.Unity.Resolve<IMediator>();
        }

        // GET: Database
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CreateDatabase(string password)
        {
            if (password != "ldyqfx")
            {
                TempData["message"] = "Password is incorrect";
                return RedirectToAction("CreateResult", new { success = false });
            }

            try
            {
               Db.CreateDb();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.ToString();

                return RedirectToAction("CreateResult", new { success = false });
            }

            TempData["message"] = "База данных создана успешно";
            return RedirectToAction("CreateResult", new {success = true });
        }

        [AllowAnonymous]
        public ActionResult CreateResult(bool success)
        {
            if (success)
            {
                ViewBag.Info = TempData["message"];
            }
            else
            {
                ViewBag.Error = TempData["message"];
            }

            return View("Index");
        }

        public ActionResult Clear()
        {
            try
            {
                _mediator.Send(new ClearDatabaseCommand());
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.ToString();
                return RedirectToAction("CreateResult", new { success = false });
            }

            TempData["message"] = "Очитка данных произведена успешно";
            return RedirectToAction("CreateResult", new { success = true });
        }
    }
}