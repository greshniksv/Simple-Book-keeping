using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MediatR;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Commands;
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
            var userId = ((UserIndentity)HttpContext.User.Identity).Id;
            IList<CostSpendDetailModel> costSpend =
                 _mediator.Send(new GetActiveCostSpendDetailsQuery { UserId = userId, CostId = costId });

            return View(costSpend);
        }

        public ActionResult Add(AddSpendModel model)
        {
            var userId = ((UserIndentity)HttpContext.User.Identity).Id;

            if (ModelState.IsValid)
            {
                _mediator.Send(new SaveSpendCommand {SpendModels = new[] {model}, UserId = userId });
            }

            return RedirectToAction("Index", new { costId = model.CostId });
        }
        public ActionResult Update(AddSpendModel[] addSpendModels)
        {
            var userId = ((UserIndentity)HttpContext.User.Identity).Id;

            _mediator.Send(new SaveSpendCommand { SpendModels = addSpendModels, UserId = userId });

            return RedirectToAction("Index", new { costId = addSpendModels.First().CostId });
        }
    }
}