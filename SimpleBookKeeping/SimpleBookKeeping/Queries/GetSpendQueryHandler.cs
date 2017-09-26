using System.Linq;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetSpendQueryHandler : IRequestHandler<GetSpendQuery, SpendModel>
    {
        public SpendModel Handle(GetSpendQuery message)
        {
            using (var session = Db.Session)
            {
                Spend spend = session.QueryOver<Spend>().Where(x => x.Id == message.SpendId).List().First();
                var spendModel = AutoMapperConfig.Mapper.Map<SpendModel>(spend);
                return spendModel;
            }
        }
    }
}