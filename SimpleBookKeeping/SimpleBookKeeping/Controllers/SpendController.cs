using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MediatR;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Commands;
using SimpleBookKeeping.Extensions;
using SimpleBookKeeping.Models;
using SimpleBookKeeping.Queries;


namespace SimpleBookKeeping.Controllers
{
    public class SpendController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller" /> class.</summary>
        public SpendController()
        {
            _mediator = MvcApp.Unity.Resolve<IMediator>();
        }

        // GET: Spend
        public ActionResult Index(Guid costId)
        {
            var userId = HttpContext.UserId();
            IList<CostSpendDetailModel> costSpend =
                 _mediator.Send(new GetActiveCostSpendDetailsQuery { UserId = userId, CostId = costId });

            ViewBag.UserId = userId;
            return View(costSpend);
        }

        public ActionResult Add(AddSpendModel model)
        {
            var userId = HttpContext.UserId();

            if (ModelState.IsValid)
            {
                _mediator.Send(new SaveSpendCommand {SpendModels = new[] {model}, UserId = userId });
            }

            return RedirectToAction("Index", new { costId = model.CostId });
        }

        public ActionResult AddCredit(AddCreditSpendModel model)
        {
            var userId = HttpContext.UserId();

            if (ModelState.IsValid)
            {
                _mediator.Send(new SaveCreditSpendCommand { SpendModels = new[] { model }, UserId = userId });
            }

            return RedirectToAction("Index", new { costId = model.CostId });
        }

        public ActionResult Update(AddSpendModel[] addSpendModels)
        {
            var userId = HttpContext.UserId();

            _mediator.Send(new SaveSpendCommand { SpendModels = addSpendModels, UserId = userId });

            return RedirectToAction("Index", new { costId = addSpendModels.First().CostId });
        }
    }
}