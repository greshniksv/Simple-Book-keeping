using System;
using System.Collections.Generic;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetCreditSpends : IRequest<IList<SpendCreditInfoModel>>
    {
        public Guid UserId { get; set; }
        public Guid CostId { get; set; }
    }
}