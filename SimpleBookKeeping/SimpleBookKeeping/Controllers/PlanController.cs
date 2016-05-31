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
            IList<Plan> plans;
            using (var session = Db.Session)
            {
                plans = session.QueryOver<Plan>().List();
            }

            return View(plans);
        }

        // GET: View
        public ActionResult View(Guid id)
        {
            IList<Plan> plans;
            using (var session = Db.Session)
            {
                plans = session.QueryOver<Plan>().Where(p => p.Id == id).List();
            }

            if (!plans.Any())
            {
                throw new PlanNotFoundException($"Plan id: {id}");
            }

            PlanModel model = AutoMapperConfig.Mapper.Map<PlanModel>(plans.First());

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
            Plan plan;
            using (var session = Db.Session)
            {
                plan = session.QueryOver<Plan>().Where(p => p.Id == id).List().FirstOrDefault();
                if (plan == null)
                {
                    return RedirectToAction("Index");
                }

                plan.User = null;
                plan.Costs.Clear();
            }

            using (var session = Db.Session)
            using (var transaction = session.BeginTransaction())
            {

                   
                session.Delete(plan);

                transaction.Commit();
            }

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
                Plan plan;
                User currentUser;
                using (var session = Db.Session)
                {
                    plan = session.QueryOver<Plan>()
                   .Where(p => p.Id == model.Id).List().FirstOrDefault();

                    currentUser =
                        session.QueryOver<User>()
                            .Where(x => x.Id == ((UserIndentity) HttpContext.User.Identity).Id)
                            .List().FirstOrDefault();
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

            return View("View", model);
        }


    }
}