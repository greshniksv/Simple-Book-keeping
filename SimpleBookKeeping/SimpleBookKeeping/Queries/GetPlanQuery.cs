using System;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetPlanQuery : IRequest<PlanModel>
    {
        public Guid PlanId { get; set; }
    }
}