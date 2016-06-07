using System.Collections.Generic;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetPlansQueryHandler : IRequestHandler<GetPlansQuery, IList<PlanModel>>
    {
        public IList<PlanModel> Handle(GetPlansQuery message)
        {
            IList<PlanModel> planModels;
            using (var session = Db.Session)
            {
                var plans = session.QueryOver<Plan>().List();
                planModels = AutoMapperConfig.Mapper.Map<IList<PlanModel>>(plans);
            }

            return planModels;
        }
    }
}