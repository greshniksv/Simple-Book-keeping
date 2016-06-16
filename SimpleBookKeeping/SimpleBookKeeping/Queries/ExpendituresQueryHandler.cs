using System;
using System.Collections.Generic;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class ExpendituresQueryHandler : IRequestHandler<ExpendituresQuery, IList<ExpenditureModel>>
    {
        /// <summary>Handles a request</summary>
        /// <param name="message">The request message</param>
        /// <returns>Response from the request</returns>
        public IList<ExpenditureModel> Handle(ExpendituresQuery message)
        {
            if (message.CostId == Guid.Empty || message.UserId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(message));
            }

            IList<ExpenditureModel> models;
            using (var session = Db.Session)
            {
                var items = session.QueryOver<Expenditure>()
                    .Where(x => x.User.Id == message.UserId && x.Cost.Id == message.CostId).List();

                models = AutoMapperConfig.Mapper.Map<IList<ExpenditureModel>>(items);
            }

            return models;
        }
    }
}