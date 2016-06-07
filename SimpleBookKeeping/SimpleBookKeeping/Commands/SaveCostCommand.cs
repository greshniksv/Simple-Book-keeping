using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Commands
{
    public class SaveCostCommand : IRequest<bool>
    {
        public CostModel Cost { get; set; }
    }
}