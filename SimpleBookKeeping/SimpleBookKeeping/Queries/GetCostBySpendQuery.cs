using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetCostBySpendQuery : IRequest<Guid>
    {
        public Guid SpendId { get; set; }
    }
}