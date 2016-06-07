using System.Linq;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Exceptions;

namespace SimpleBookKeeping.Commands
{
    public class RemoveCostCommandHandler : IRequestHandler<RemoveCostCommand, bool>
    {
        public bool Handle(RemoveCostCommand message)
        {
            Cost cost;
            using (var session = Db.Session)
            {
                cost = session.QueryOver<Cost>().Where(x => x.Id == message.CostId).List().FirstOrDefault();
                if (cost == null)
                {
                    throw new CostNotFoundException(message.CostId.ToString());
                }
                cost.CostDetails.Clear();
            }

            using (var session = Db.Session)
            using (var transaction = session.BeginTransaction())
            {
                cost.Plan = null;
                session.Delete(cost);
                transaction.Commit();
            }

            return true;
        }
    }
}