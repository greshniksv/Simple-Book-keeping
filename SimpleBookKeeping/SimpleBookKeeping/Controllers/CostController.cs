using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Exceptions;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Controllers
{
    public class CostController : Controller
    {
        // GET: Cost
        public ActionResult Index(Guid planId)
        {
            Plan plan;
            using (var session = Db.Session)
            {
                plan = session.QueryOver<Plan>()
                    .Where(p => p.Id == planId).List().FirstOrDefault();
            }

            if (plan == null)
            {
                throw new PlanNotFoundException($"Plan id: {planId}");
            }

            var costDetails = new List<CostDetailModel>();

            for (DateTime i = plan.Start; i < plan.End; i = i.AddDays(1))
            {
                costDetails.Add(new CostDetailModel
                {
                    Date = i,
                    Value = 0
                });
            }

            var model = new CostModel
            {
                Name = string.Empty,
                CostDetails = costDetails
            };
            
            TempData["model"] = model;
            return RedirectToAction("Edit");
        }

        public ActionResult Edit(CostModel model)
        {
            var tempModel = TempData["model"] as CostModel;
            if (model.Id == Guid.Empty && tempModel != null)
            {
                model = tempModel;
                ModelState.Clear();
            }

            return View("Index", model);
        }

        public ActionResult Save(Guid planId)
        {
            Plan plan;
            using (var session = Db.Session)
            {
                plan = session.QueryOver<Plan>()
                    .Where(p => p.Id == planId).List().FirstOrDefault();
            }

            if (plan == null)
            {
                throw new PlanNotFoundException($"Plan id: {planId}");
            }

            return View("Index");
        }


    }
}