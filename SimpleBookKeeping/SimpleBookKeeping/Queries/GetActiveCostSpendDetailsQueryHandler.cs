using System;
using System.Collections.Generic;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetActiveCostSpendDetailsQueryHandler : IRequestHandler<GetActiveCostSpendDetailsQuery, IList<CostSpendDetailModel>>
    {
        private IMediator _mediator;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public GetActiveCostSpendDetailsQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Handles a request</summary>
        /// <param name="message">The request message</param>
        /// <returns>Response from the request</returns>
        public IList<CostSpendDetailModel> Handle(GetActiveCostSpendDetailsQuery message)
        {
            var activePlans = _mediator.Send(new GetPlansQuery { IsActive = true });


            return null;
        }
    }
}