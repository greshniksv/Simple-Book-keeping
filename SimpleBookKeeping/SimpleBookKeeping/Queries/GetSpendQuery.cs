using System;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetSpendQuery : IRequest<SpendModel>
    {
        public Guid SpendId { get; set; }
    }
}