using System.Collections.Generic;
using System.Web.Mvc;
using MediatR;
using SimpleBookKeeping.Queries;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Extensions;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller" /> class.</summary>
        public HomeController()
        {
            _mediator = MvcApp.Unity.Resolve<IMediator>();
        }

        // GET: Home
        public ActionResult Index()
        {
            var userId = HttpContext.UserId();

            ViewBag.Title = "SimpleBookKeeping";

            return View();
        }
    }
}