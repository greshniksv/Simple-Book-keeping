using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using SimpleBookKeeping.Attributes;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Exceptions;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Controllers
{
    //[HandleAllError]
    public class PlanController : Controller
    {
        // GET: Plan
        public ActionResult Index()
        {
            var plans = Db.Session.QueryOver<Plan>().List<Plan>();

            return View(plans);
        }

        // GET: Edit
        public ActionResult EditById(Guid id)
        {
            var plans = Db.Session.QueryOver<Plan>().Where(p=>p.Id == id).List();

            if (!plans.Any())
            {
                throw new PlanNotFoundException($"Plan id: {id}");
            }

            PlanModel model = AutoMapperConfig.Mapper.Map<PlanModel>(plans.First());

            TempData["model"] = model;
            return RedirectToAction("Edit");
        }

        public ActionResult Edit(PlanModel model)
        {
            var tempModel = TempData["model"] as PlanModel;
            if (model.Start == DateTime.MinValue && tempModel != null)
            {
                model = tempModel;
                ModelState.Clear();
            }

            return View("Edit", model);
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
                Plan plan;
                User currentUser;
                using (var session = Db.Session)
                {
                    plan = session.QueryOver<Plan>()
                   .Where(p => p.Id == model.Id).List().FirstOrDefault();

                    currentUser =
                        session.QueryOver<User>()
                            .Where(x => x.Id == ((UserIndentity) HttpContext.User.Identity).Id).List().FirstOrDefault();
                }

                if (plan == null)
                {
                    plan = new Plan
                    {
                        User = currentUser
                    };
                }
                AutoMapperConfig.Mapper.Map(model, plan);

                using (var sessionInsert = Db.Session)
                using (var transaction = sessionInsert.BeginTransaction())
                {
                    sessionInsert.SaveOrUpdate(plan);
                    transaction.Commit();
                }
                
                return RedirectToAction("Index");
            }

            TempData["model"] = model;
            return RedirectToAction("Edit");
        }


    }
}