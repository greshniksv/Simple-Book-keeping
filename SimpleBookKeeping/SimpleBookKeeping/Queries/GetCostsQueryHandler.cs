using System;
using System.Collections.Generic;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Exceptions;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetCostsQueryHandler : IRequestHandler<GetCostsQuery, IList<CostModel>>
    {
        public IList<CostModel> Handle(GetCostsQuery message)
        {
            if (message.PlanId == Guid.Empty)
            {
                throw new PlanNotFoundException(message.PlanId.ToString());
            }

            IList<CostModel> costModels;
            using (var session = Db.Session)
            {
                var costs = session.QueryOver<Cost>().Where(x => x.Plan.Id == message.PlanId).List();
                costModels = AutoMapperConfig.Mapper.Map<IList<CostModel>>(costs);
            }

            return costModels;
        }
    }
}