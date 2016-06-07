using System;
using MediatR;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Commands
{
    public class AddUpdatePlanCommand : IRequest<bool>
    {
        public PlanModel PlanModel { get; set; }
        public Guid UserId { get; set; }
    }
}