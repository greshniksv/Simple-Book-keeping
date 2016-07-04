using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MediatR;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Attributes;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Commands;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Extensions;
using SimpleBookKeeping.Models;
using SimpleBookKeeping.Queries;

namespace SimpleBookKeeping.Controllers
{
    [Authorize]
    [HandleAllError]
    public class PlanController : Controller
    {
        private readonly IMediator _mediator;

        public PlanController()
        {
            _mediator = MvcApp.Unity.Resolve<IMediator>();
        }

        // GET: Plan
        public ActionResult Index()
        {
            var plans = _mediator.Send(new GetPlansQuery { UserId = HttpContext.UserId() });
            return View(plans);
        }

        // GET: View
        public ActionResult View(Guid id)
        {
            var model = _mediator.Send(new GetPlanQuery { PlanId = id });
            var users = _mediator.Send(new GetUsersQuery());

            ViewBag.Users = users;
            return View("View", model);
        }

        // GET: Create
        public ActionResult Create()
        {
            PlanModel model = new PlanModel
            {
                Balance = 0,
                Start = DateTime.Now,
                End = DateTime.Now.AddMonths(1),
                Name = string.Empty,
                UserMembers = new List<Guid>(),
                Id = Guid.Empty
            };

            var users = _mediator.Send(new GetUsersQuery());

            ViewBag.Users = users;
            return View("View", model);
        }

        // GET: Remove
        public ActionResult Remove(Guid id)
        {
            _mediator.Send(new RemovePlanCommand { PlanId = id });

            return RedirectToAction("Index");
        }

        public ActionResult Save(PlanModel model)
        {
            if (model.Start == DateTime.MinValue && model.End == DateTime.MinValue)
            {
                return RedirectToAction("Index");
            }

            PlanModel oldPlan = null;
            if (model.Id != Guid.Empty)
            {
                oldPlan = _mediator.Send(new GetPlanQuery { PlanId = model.Id });
            }

            if (model.Start >= model.End)
            {
                ModelState
                    .AddModelError(nameof(model.Start),
                            "Дата начала должна быть меньше даты конца");
            }
            IList<CostModel> costs = null;
            if (model.Id != Guid.Empty)
            {
                costs = _mediator.Send(new GetCostsQuery { PlanId = model.Id });
            }

            if (costs != null && costs.Any() && oldPlan != null &&
                (oldPlan.Start.Date != model.Start.Date || oldPlan.End.Date != model.End.Date))
            {
                ModelState
                    .AddModelError(nameof(model.Start),
                            "Нельзя изменить дату начала, так как существуют расходы");
                ModelState
                    .AddModelError(nameof(model.End),
                            "Нельзя изменить дату завершения, так как существуют расходы");
            }

            if (ModelState.IsValid)
            {
                _mediator.Send(new SavePlanCommand
                {
                    PlanModel = model,
                    UserId = ((UserIndentity)HttpContext.User.Identity).Id
                });

                return RedirectToAction("Index");
            }

            var users = _mediator.Send(new GetUsersQuery());
            ViewBag.Users = users;
            return View("View", model);
        }

    }
}