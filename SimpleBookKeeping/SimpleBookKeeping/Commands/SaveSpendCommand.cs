using System;
using System.Collections.Generic;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Commands
{
    public class SaveSpendCommand : IRequest
    {
        public IReadOnlyCollection<AddSpendModel> SpendModels { get; set; }

        public Guid UserId { get; set; }
    }
}