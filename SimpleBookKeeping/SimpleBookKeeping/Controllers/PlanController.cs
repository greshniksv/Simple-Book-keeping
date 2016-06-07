﻿using System;
using System.Web.Mvc;
using MediatR;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Attributes;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Commands;
using SimpleBookKeeping.Models;
using SimpleBookKeeping.Queries;

namespace SimpleBookKeeping.Controllers
{
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
            var plans = _mediator.Send(new GetPlansQuery());
            return View(plans);
        }

        // GET: View
        public ActionResult View(Guid id)
        {
            var model = _mediator.Send(new GetPlanQuery { PlanId = id});

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
                Name = string.Empty
            };

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
            if (model.Start >= model.End)
            {
                ModelState
                    .AddModelError(nameof(model.Start),
                            "Дата начала должна быть меньше даты конца");
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

            return View("View", model);
        }

    }
}