using System;
using MediatR;

namespace SimpleBookKeeping.Commands
{
    public class RemoveCostCommand : IRequest<bool>
    {
        public Guid CostId { get; set; }
    }
}