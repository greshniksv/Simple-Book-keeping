using System.Linq;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Exceptions;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetCostQueryHandler : IRequestHandler<GetCostQuery, CostModel>
    {
        public CostModel Handle(GetCostQuery message)
        {
            CostModel item;

            using (var session = Db.Session)
            {
                var cost = session.QueryOver<Cost>()
                    .Where(x => x.Id == message.CostId).List().FirstOrDefault();

                if (cost == null)
                {
                    throw new CostNotFoundException(message.CostId.ToString());
                }

                item = new CostModel();
                AutoMapperConfig.Mapper.Map(cost, item);
            }

            return item;
        }
    }
}