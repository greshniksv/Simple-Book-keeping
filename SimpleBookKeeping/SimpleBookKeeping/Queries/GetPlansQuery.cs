using System.Collections.Generic;
using System.ComponentModel;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetPlansQuery : IRequest<IList<PlanModel>>
    {
        [DefaultValue(false)]
        public bool ShowDeleted { get; set; }
    }
}