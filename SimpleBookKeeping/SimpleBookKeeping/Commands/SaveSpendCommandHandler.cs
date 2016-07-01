using System;
using System.Linq;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;

namespace SimpleBookKeeping.Commands
{
    public class SaveSpendCommandHandler : RequestHandler<SaveSpendCommand>
    {
        /// <summary>Handles a void request</summary>
        /// <param name="message">The request message</param>
        protected override void HandleCore(SaveSpendCommand message)
        {
            if (!message.SpendModels.Any())
            {
                return;
            }

            using (var session = Db.Session)
            using (var transaction = session.BeginTransaction())
            {
                foreach (var spendModel in message.SpendModels)
                {
                    if (spendModel.Id == null || spendModel.Id == Guid.Empty)
                    {
                        var costDetail = 
                            session.QueryOver<CostDetail>().Where(x => x.Id == spendModel.CostDetailId).List().First();

                        // New Spend
                        Spend spend = new Spend
                        {
                            User = session.QueryOver<User>().Where(x => x.Id == message.UserId).List().First(),
                            Comment = spendModel.Comment,
                            CostDetail = session.QueryOver<CostDetail>().Where(x => x.Id == spendModel.CostDetailId).List().First(),
                            Value = spendModel.Value,
                            OrderId = costDetail.Spends.Count
                        };

                        session.Save(spend);
                    }
                    else
                    {
                        Spend spend = session.QueryOver<Spend>().Where(x => x.Id == spendModel.Id).List().First();

                        if (spendModel.Value == 0 && spendModel.Comment == null)
                        {
                            // Remove Spend
                            session.Delete(spend);
                        }
                        else
                        {
                            // Update Spend
                            spend.User = session.QueryOver<User>().Where(x => x.Id == message.UserId).List().First();
                            spend.Comment = spendModel.Comment;
                            spend.CostDetail = session.QueryOver<CostDetail>().Where(x => x.Id == spendModel.CostDetailId).List().First();
                            spend.Value = spendModel.Value;

                            session.SaveOrUpdate(spend);
                        }
                    }
                }
                transaction.Commit();
            }
        }
    }
}