using System;
using System.Collections.Generic;
using System.ComponentModel;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetCostsQuery : IRequest<IList<CostModel>>
    {
        public Guid PlanId { get; set; }

        [DefaultValue(false)]
        public bool ShowDeleted { get; set; }
    }
}