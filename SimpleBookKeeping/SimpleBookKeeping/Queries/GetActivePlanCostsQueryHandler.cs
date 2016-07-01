using System.Collections.Generic;
using System.Linq;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetActivePlanCostsQueryHandler : IRequestHandler<GetActivePlanCostsQuery, IReadOnlyCollection<PlanCostsModel>>
    {
        /// <summary>Handles a request</summary>
        /// <param name="message">The request message</param>
        /// <returns>Response from the request</returns>
        public IReadOnlyCollection<PlanCostsModel> Handle(GetActivePlanCostsQuery message)
        {
            List<PlanCostsModel> planCostsModels;
            using (var session = Db.Session)
            {
                // Note: Find by creator and by member in plan.

                var plans = session.QueryOver<Plan>().Where(x => 
                x.User.Id == message.UserId || 
                x.PlanMembers.Any(p=>p.User.Id == message.UserId) 
                ).List();

                planCostsModels = AutoMapperConfig.Mapper.Map<List<PlanCostsModel>>(plans);
            }
            return planCostsModels;
        }
    }
}