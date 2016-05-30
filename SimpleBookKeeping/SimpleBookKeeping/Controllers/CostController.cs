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
            if (planId == Guid.Empty)
            {
                planId = new Guid("c23070b3-ddec-4eb0-a5c2-0fcc9a13d5f6");
            }

            IList<CostModel> costModels = new List<CostModel>();
            using (var session = Db.Session)
            {
                var costs = session.QueryOver<Cost>().Where(x => x.Plan.Id == planId).List();

                foreach (var cost in costs)
                {
                    var item = new CostModel();
                    AutoMapperConfig.Mapper.Map(cost, item);
                    costModels.Add(item);
                }
            }
            
            ViewBag.PlanId = planId;
            return View("Index", costModels);
        }

        public ActionResult EditById(Guid id)
        {
            Cost cost;
            CostModel item;
            using (var session = Db.Session)
            {
                cost = session.QueryOver<Cost>()
                    .Where(x => x.Id == id).List().FirstOrDefault();

                if (cost == null)
                {
                    throw new CostNotFoundException(id.ToString());
                }

                item = new CostModel();
                AutoMapperConfig.Mapper.Map(cost, item);
            }

            TempData["model"] = item;
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

            return View("Edit", model);
        }

        public ActionResult Save(CostModel model)
        {
            Cost cost;
            Plan plan;

            using (var session = Db.Session)
            {
                plan = session.QueryOver<Plan>()
               .Where(p => p.Id == model.PlanId).List().FirstOrDefault();
            }
           
            if (model.Id != Guid.Empty)
            {
                using (var session = Db.Session)
                {
                    cost = session.QueryOver<Cost>()
                        .Where(x => x.Id == model.Id).List().FirstOrDefault();
                }

                if (cost == null)
                {
                    throw new CostNotFoundException(model.Id.ToString());
                }
            }
            else
            {
                cost = new Cost();
            }
            
            AutoMapperConfig.Mapper.Map(model, cost);
            cost.Plan = plan;

            foreach (var costDetailModel in model.CostDetails)
            {
                var detail = new CostDetail();
                AutoMapperConfig.Mapper.Map(costDetailModel, detail);
                detail.Cost = cost;
                cost.CostDetails.Add(detail);
            }

            using (var session = Db.Session)
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(cost);
                transaction.Commit();
            }
           
            return View("Index");
        }

        private CostModel CreateNew(Guid planId)
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
                CostDetails = costDetails,
                PlanId = planId
            };

            return model;
        }

    }
}