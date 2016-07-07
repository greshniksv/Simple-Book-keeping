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
                var costs = plan.Costs.Where(x => x.Deleted == false).ToList();

                var passedDays = (DateTime.Now.Date - plan.Start.Date).Days;
                var totalDays = (plan.End.Date - plan.Start.Date).Days;

                planStatusModel.Id = plan.Id;
                planStatusModel.Name = plan.Name;
                planStatusModel.Progress = passedDays * 100 / totalDays;
                
                foreach (var cost in costs.OrderBy(x => x.Name))
                {
                    int balance = 0;
                    // (That you can spend) - (you spend)
                    foreach (var costDetail in cost.CostDetails.OrderBy(x=>x.Date))
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

                // Balance on start minus sum of planed costs
                planStatusModel.Rest = plan.Balance - allSpend;
                planStatusModel.BalanceToEnd = plan.Balance - costs.Sum(x => x.CostDetails.Sum(d => d.Value));
                if (planStatusModel.Rest < 0)
                {
                    // If we exceed the plan, we must take away this exceed sum.
                    planStatusModel.BalanceToEnd += planStatusModel.Rest;
                }
                planStatusModel.Balance = costStatusModels.Sum(x => x.Balance);
            }

            planStatusModel.CostStatusModels = costStatusModels;
            return planStatusModel;
        }
    }
}