using System;

namespace SimpleBookKeeping.Models
{
    public class SpendModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid CostId { get; set; }

        public DateTime Date { get; set; }

        public int Value { get; set; }

        public string Comment { get; set; }
    }
}