using System;
using System.Collections.Generic;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetCostsQuery : IRequest<IList<CostModel>>
    {
        public Guid PlanId { get; set; }
    }
}