using System;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetPlanStatusQuery : IRequest<PlanStatusModel>
    {
        public Guid PlanId { get; set; }
    }
}