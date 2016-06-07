using System.Linq;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;

namespace SimpleBookKeeping.Commands
{
    public class SavePlanCommandHandler : IRequestHandler<SavePlanCommand, bool>
    {
        /// <summary>Handles a request</summary>
        /// <param name="message">The request message</param>
        /// <returns>Response from the request</returns>
        public bool Handle(SavePlanCommand message)
        {
            Plan plan;
            User currentUser;
            using (var session = Db.Session)
            {
                plan = session.QueryOver<Plan>()
               .Where(p => p.Id == message.PlanModel.Id).List().FirstOrDefault();

                currentUser =
                    session.QueryOver<User>()
                        .Where(x => x.Id == message.UserId)
                        .List().FirstOrDefault();
            }

            if (plan == null)
            {
                plan = new Plan
                {
                    User = currentUser
                };
            }
            AutoMapperConfig.Mapper.Map(message.PlanModel, plan);

            using (var sessionInsert = Db.Session)
            using (var transaction = sessionInsert.BeginTransaction())
            {
                sessionInsert.SaveOrUpdate(plan);
                transaction.Commit();
            }

            return true;
        }
    }
}