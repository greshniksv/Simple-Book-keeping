﻿using System.Collections.Generic;
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
            List<PlanCostsModel> planCostsModels = new List<PlanCostsModel>();
            using (var session = Db.Session)
            {
                // Note: Find by creator and by member in plan.
                var plansByCreator = session.QueryOver<Plan>().Where(x => x.User.Id == message.UserId).List();
                var plansByMember = session.QueryOver<PlanMember>().Where(x => x.User.Id == message.UserId).Select(x=>x.Plan).List<Plan>();

                planCostsModels.AddRange(AutoMapperConfig.Mapper.Map<List<PlanCostsModel>>(plansByCreator));
                planCostsModels.AddRange(AutoMapperConfig.Mapper.Map<List<PlanCostsModel>>(plansByMember));
            }
            return planCostsModels;
        }
    }
}