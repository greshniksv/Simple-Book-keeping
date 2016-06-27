using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetActiveCostSpendDetailsQueryHandler : IRequestHandler<GetActiveCostSpendDetailsQuery, IList<CostSpendDetailModel>>
    {
        private readonly IMediator _mediator;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public GetActiveCostSpendDetailsQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Handles a request</summary>
        /// <param name="message">The request message</param>
        /// <returns>Response from the request</returns>
        public IList<CostSpendDetailModel> Handle(GetActiveCostSpendDetailsQuery message)
        {
            IList<CostSpendDetailModel> costSpendDetailModels = new List<CostSpendDetailModel>();

            var activePlans = _mediator.Send(new GetPlansQuery { IsActive = true });
            
            using (var session = Db.Session)
            {
                foreach (var activePlan in activePlans)
                {
                    var costs = session.QueryOver<Cost>().Where(x => x.Plan.Id == activePlan.Id).List();

                    if (message.CostId != Guid.Empty)
                    {
                        costs = costs.Where(x => x.Id == message.CostId).ToList();
                    }

                    foreach (var cost in costs)
                    {
                        foreach (var costDetail in cost.CostDetails)
                        {
                            var item = new CostSpendDetailModel
                            {
                                CostId = cost.Id,
                                CostName = cost.Name,
                                DetailId = costDetail.Id,
                                Date = costDetail.Date,
                                Value = costDetail.Value,
                                Spends = new List<SpendModel>()
                            };

                            var spends = session.QueryOver<Spend>()
                                .Where(x => x.Cost.Id == cost.Id && x.Date.Date == costDetail.Date.Date).List();

                            var spendModels = AutoMapperConfig.Mapper.Map<IList<SpendModel>>(spends);
                            ((List<SpendModel>)item.Spends).AddRange(spendModels);

                            costSpendDetailModels.Add(item);
                        }
                    }
                }
            }

            return costSpendDetailModels;
        }
    }
}