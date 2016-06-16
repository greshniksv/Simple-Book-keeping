using System;
using System.Collections.Generic;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class ExpendituresQuery : IRequest<IList<ExpenditureModel>>
    {
        public Guid UserId { get; set; }
        public Guid CostId { get; set; }
    }
}