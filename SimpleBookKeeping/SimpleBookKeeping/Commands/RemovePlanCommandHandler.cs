using System.Linq;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;

namespace SimpleBookKeeping.Commands
{
    public class RemovePlanCommandHandler : IRequestHandler<RemovePlanCommand, bool>
    {
        public bool Handle(RemovePlanCommand message)
        {
            Plan plan;
            using (var session = Db.Session)
            {
                plan = session.QueryOver<Plan>().Where(p => p.Id == message.PlanId).List().FirstOrDefault();
                if (plan == null)
                {
                    return false;
                }
            }

            using (var session = Db.Session)
            using (var transaction = session.BeginTransaction())
            {
                plan.Deleted = true;
                session.Update(plan);
                transaction.Commit();
            }
            return true;
        }
    }
}