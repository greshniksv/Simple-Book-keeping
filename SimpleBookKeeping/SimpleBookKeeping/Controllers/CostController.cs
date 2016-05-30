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

        public ActionResult Create(Guid planId)
        {
            var model = CreateNew(planId);
            return View("Edit", model);
        }

        public ActionResult View(Guid id)
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

            return View("Edit", item);
        }

        public ActionResult Save(CostModel model)
        {
            if (ModelState.IsValid)
            {
                SaveModel(model);
                return RedirectToAction("Index", new { planId = model.PlanId });
            }

            return View("Edit", model);
        }

        private void SaveModel(CostModel model)
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

                    if (cost == null)
                    {
                        throw new CostNotFoundException(model.Id.ToString());
                    }

                    cost.CostDetails.Clear();
                }

                using (var session = Db.Session)
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(cost);
                    transaction.Commit();
                }
            }
            else
            {
                cost = new Cost { Plan = plan };
            }

            AutoMapperConfig.Mapper.Map(model, cost);

            using (var session = Db.Session)
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(cost);
                transaction.Commit();
            }

            using (var session = Db.Session)
            using (var transaction = session.BeginTransaction())
            {

                foreach (var costDetailModel in model.CostDetails)
                {
                    var detail = new CostDetail();
                    AutoMapperConfig.Mapper.Map(costDetailModel, detail);
                    detail.Cost = cost;
                    session.SaveOrUpdate(detail);
                }

                transaction.Commit();
            }
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