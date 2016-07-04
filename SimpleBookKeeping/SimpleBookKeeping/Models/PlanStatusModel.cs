using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBookKeeping.Models
{
    public class PlanStatusModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public int Rest { get; set; }

        public IReadOnlyCollection<CostStatusModel> CostStatusModels { get; set; }
    }
}