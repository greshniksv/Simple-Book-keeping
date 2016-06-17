using System;
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
                IList<Plan> plans;
                var planQuery = session.QueryOver<Plan>();

                if (message.ShowDeleted != null)
                {
                    planQuery = planQuery.Where(x => x.Deleted == message.ShowDeleted);
                }

                if (message.IsActive != null)
                {
                    var now = DateTime.Now;
                    planQuery = planQuery.Where(x => x.Start <= now && x.End >= now);
                }

                plans = planQuery.List();
                planModels = AutoMapperConfig.Mapper.Map<IList<PlanModel>>(plans);
            }

            return planModels;
        }
    }
}