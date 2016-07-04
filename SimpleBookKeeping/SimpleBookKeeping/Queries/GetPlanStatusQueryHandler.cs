using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetPlanStatusQueryHandler : IRequestHandler<GetPlanStatusQuery, PlanStatusModel>
    {

        /// <summary>Handles a request</summary>
        /// <param name="message">The request message</param>
        /// <returns>Response from the request</returns>
        public PlanStatusModel Handle(GetPlanStatusQuery message)
        {
            PlanStatusModel planStatusModel = new PlanStatusModel();
            List<CostStatusModel> costStatusModels = new List<CostStatusModel>();
            using (var session = Db.Session)
            {
                int allSpend = 0;
                var plan = session.QueryOver<Plan>().Where(x => x.Id == message.PlanId).List().FirstOrDefault();
                if (plan == null)
                {
                    throw new Exception("GetPlanStatusQuery. Plan not found.");
                }

                planStatusModel.Id = plan.Id;
                planStatusModel.Name = plan.Name;

                foreach (var cost in plan.Costs.OrderBy(x=>x.Name))
                {
                    int balance = 0;
                    // (That you can spend) - (you spend)
                    foreach (var costDetail in cost.CostDetails)
                    {
                        if (costDetail.Date.Date <= DateTime.Now.Date)
                        {
                            allSpend += costDetail.Spends.Sum(x => x.Value);
                            balance += costDetail.Value - costDetail.Spends.Sum(x => x.Value);
                        }
                    }

                    costStatusModels.Add(new CostStatusModel
                    {
                        Id = cost.Id,
                        Name = cost.Name,
                        Balance = balance
                    });
                }
                planStatusModel.Rest = plan.Balance - allSpend;
                planStatusModel.Balance = costStatusModels.Sum(x => x.Balance);
            }
            
            planStatusModel.CostStatusModels = costStatusModels;
            return planStatusModel;
        }
    }
}