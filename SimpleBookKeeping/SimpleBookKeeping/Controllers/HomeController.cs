using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediatR;
using SimpleBookKeeping.Queries;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IMediator _mediator;

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller" /> class.</summary>
        public HomeController()
        {
            _mediator = MvcApp.Unity.Resolve<IMediator>();
        }

        // GET: Home
        public ActionResult Index()
        {
            var userId = ((UserIndentity)HttpContext.User.Identity).Id;

            ViewBag.Title = "SimpleBookKeeping";
            IReadOnlyCollection<PlanCostsModel> planCosts = _mediator.Send(new GetActivePlanCostsQuery { UserId = userId });

            return View();
        }
    }
}