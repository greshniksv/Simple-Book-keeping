using System;
using System.Collections.Generic;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Commands
{
    public class SaveCreditSpendCommand : IRequest
    {
        public IReadOnlyCollection<AddCreditSpendModel> SpendModels { get; set; }

        public Guid UserId { get; set; }
    }
}