using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Exceptions;

namespace SimpleBookKeeping.Commands
{
    public class SaveCostCommandHandler : IRequestHandler<SaveCostCommand, bool>
    {
        public bool Handle(SaveCostCommand message)
        {
            Cost cost;
            Plan plan;
            List<CostDetail> costDetails = null;

            using (var session = Db.Session)
            {
                plan = session.QueryOver<Plan>()
               .Where(p => p.Id == message.Cost.PlanId).List().FirstOrDefault();
            }

            if (message.Cost.Id != Guid.Empty)
            {
                using (var session = Db.Session)
                {
                    cost = session.QueryOver<Cost>()
                        .Where(x => x.Id == message.Cost.Id).List().FirstOrDefault();

                    if (cost == null)
                    {
                        throw new CostNotFoundException(message.Cost.Id.ToString());
                    }
                    costDetails = cost.CostDetails.ToList();
                }
            }
            else
            {
                cost = new Cost { Plan = plan };
            }

            AutoMapperConfig.Mapper.Map(message.Cost, cost);

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

                foreach (var costDetailModel in message.Cost.CostDetails)
                {
                    var detail = new CostDetail();
                    AutoMapperConfig.Mapper.Map(costDetailModel, detail);
                    detail.Cost = cost;
                    session.SaveOrUpdate(detail);
                }

                transaction.Commit();
            }

            return true;
        }
    }
}