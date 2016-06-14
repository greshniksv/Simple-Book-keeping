using System.Collections.Generic;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetPlansQuery : IRequest<IList<PlanModel>>
    {
        public bool ShowDeleted { get; set; }
    }
}