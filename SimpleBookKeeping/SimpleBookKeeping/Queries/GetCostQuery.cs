using System;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetCostQuery : IRequest<CostModel>
    {
        public Guid CostId { get; set; }
    }
}