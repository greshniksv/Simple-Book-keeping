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
                throw new PlanNotFoundException(planId.ToString());
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
            return View("View", model);
        }

        public ActionResult View(Guid id)
        {
            CostModel item;
            
            using (var session = Db.Session)
            {
                var cost = session.QueryOver<Cost>()
                    .Where(x => x.Id == id).List().FirstOrDefault();

                if (cost == null)
                {
                    throw new CostNotFoundException(id.ToString());
                }

                item = new CostModel();
                AutoMapperConfig.Mapper.Map(cost, item);
            }

            return View("View", item);
        }

        public ActionResult Save(CostModel model)
        {
            foreach (var costDetailModel in model.CostDetails)
            {
                if (costDetailModel.Value == null)
                    costDetailModel.Value = 0;
            }

            if (ModelState.IsValid)
            {
                SaveModel(model);
                return RedirectToAction("Index", new { planId = model.PlanId });
            }

            return View("View", model);
        }

        private void SaveModel(CostModel model)
        {
            Cost cost;
            Plan plan;
            List<CostDetail> costDetails = null;

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
                    costDetails = cost.CostDetails.ToList();
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

            // Remove old details
            if (costDetails != null)
            {
                using (var session = Db.Session)
                using (var transaction = session.BeginTransaction())
                {
                    foreach (var costDetail in costDetails)
                    {
                        costDetail.Cost = null;
                        session.Delete(costDetail);
                    }
                    transaction.Commit();
                }
            }

            // Insert new details
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