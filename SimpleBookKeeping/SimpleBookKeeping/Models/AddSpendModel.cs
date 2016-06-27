using System;

namespace SimpleBookKeeping.Models
{
    public class AddSpendModel
    {
        public Guid CostId { get; set; }

        public Guid? CostDetailId { get; set; }

        public int Value { get; set; }

        public string Comment { get; set; }

    }
}