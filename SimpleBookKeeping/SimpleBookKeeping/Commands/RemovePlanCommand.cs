using System;
using MediatR;

namespace SimpleBookKeeping.Commands
{
    public class RemovePlanCommand : IRequest<bool>
    {
        public Guid PlanId { get; set; }
    }
}