using System;
using System.Linq;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;

namespace SimpleBookKeeping.Queries
{
    public class GetCostBySpendQueryHandler : IRequestHandler<GetCostBySpendQuery, Guid>
    {
        public Guid Handle(GetCostBySpendQuery message)
        {
            using (var session = Db.Session)
            {
                Spend spend = session.QueryOver<Spend>().Where(x => x.Id == message.SpendId).List().First();
                return spend.CostDetail.Cost.Id;
            }
        }
    }
}