using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Exceptions;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Commands
{
    public class CreateCostCommandHandler : IRequestHandler<CreateCostCommand, CostModel>
    {
        public CostModel Handle(CreateCostCommand message)
        {
            Plan plan;
            using (var session = Db.Session)
            {
                plan = session.QueryOver<Plan>()
                    .Where(p => p.Id == message.PlanId).List().FirstOrDefault();
            }

            if (plan == null)
            {
                throw new PlanNotFoundException($"Plan id: {message.PlanId}");
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
                PlanId = message.PlanId
            };

            return model;
        }
    }
}