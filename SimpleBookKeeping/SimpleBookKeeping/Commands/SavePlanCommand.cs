using System;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Commands
{
    public class SavePlanCommand : IRequest<bool>
    {
        public PlanModel PlanModel { get; set; }

        public Guid UserId { get; set; }
    }
}