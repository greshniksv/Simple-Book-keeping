using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MediatR;
using SimpleBookKeeping.Queries;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Extensions;
using SimpleBookKeeping.Models;
using SimpleBookKeeping.Unility;

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

            List<PlanStatusModel> planStatusModels = new List<PlanStatusModel>();
            var activePlans = _mediator.Send(new GetPlansQuery { IsActive = true, UserId = userId });
            foreach (var activePlan in activePlans)
            {
                var palnStatus = _mediator.Send(new GetPlanStatusQuery { PlanId = activePlan.Id });
                planStatusModels.Add(palnStatus);
            }

            return View(planStatusModels.OrderBy(x=>x.Name).ToList());
        }

        public ActionResult Test()
        {
            var id = Guid.NewGuid().ToString();
            var file = @"E:\temp\DC PDF.zip";

            FileStorage storage = new FileStorage();
            storage.Save(id, System.IO.File.OpenRead(file));

            return View();
        }

    }
}