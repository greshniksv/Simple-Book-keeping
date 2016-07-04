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
        private Dictionary<Guid, User> _userCache;

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

            var activePlans = _mediator.Send(new GetPlansQuery { IsActive = true, UserId = message.UserId });

            using (var session = Db.Session)
            {
                _userCache = session.QueryOver<User>().List().ToDictionary(x => x.Id, x => x);

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

                            var spendModels = AutoMapperConfig.Mapper.Map<IList<SpendModel>>(costDetail.Spends.OrderBy(x => x.OrderId));

                            // Replace other user comment
                            foreach (var spendModel in spendModels)
                            {
                                if (spendModel.UserId != message.UserId)
                                {
                                    spendModel.Comment = GetUser(spendModel.UserId).Name;
                                }
                            }

                            ((List<SpendModel>)item.Spends).AddRange(spendModels);

                            costSpendDetailModels.Add(item);
                        }
                    }
                }
            }

            return costSpendDetailModels;
        }

        private User GetUser(Guid userId)
        {
            User user;
            if (!_userCache.TryGetValue(userId, out user))
            {
                throw new Exception("User not found!");
            }
            return user;
        }
    }
}