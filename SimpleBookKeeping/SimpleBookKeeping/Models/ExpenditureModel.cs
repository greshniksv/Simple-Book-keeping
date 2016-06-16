using System;

namespace SimpleBookKeeping.Models
{
    public class ExpenditureModel
    {
        public virtual Guid Id { get; set; }

        public virtual Guid UserId { get; set; }

        public virtual Guid CostId { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual int Value { get; set; }
    }
}