using System.Collections.Generic;
using System.Linq;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Exceptions;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetPlanQueryHandler : IRequestHandler<GetPlanQuery, PlanModel>
    {
        public PlanModel Handle(GetPlanQuery message)
        {
            IList<Plan> plans;
            using (var session = Db.Session)
            {
                plans = session.QueryOver<Plan>().Where(p => p.Id == message.PlanId).List();
            }

            if (!plans.Any())
            {
                throw new PlanNotFoundException($"Plan id: {message.PlanId}");
            }

            PlanModel model = AutoMapperConfig.Mapper.Map<PlanModel>(plans.First());

            return model;
        }
    }
}