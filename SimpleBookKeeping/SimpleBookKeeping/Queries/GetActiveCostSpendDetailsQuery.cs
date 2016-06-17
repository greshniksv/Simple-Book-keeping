﻿using System.Collections.Generic;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetActiveCostSpendDetailsQuery : IRequest<IList<CostSpendDetailModel>>
    {
    }
}