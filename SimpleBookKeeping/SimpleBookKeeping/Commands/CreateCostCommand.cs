using System;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Commands
{
    public class CreateCostCommand : IRequest<CostModel>
    {
        public Guid PlanId { get; set; }
    }
}