using System;
using System.Collections.Generic;

namespace SimpleBookKeeping.Models
{
    public class CostSpendDetailModel
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int? Value { get; set; }

        public IList<SpendModel> Spends { get; set; }
    }
}