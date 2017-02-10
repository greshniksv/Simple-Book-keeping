using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleBookKeeping.Models
{
    public class AddCreditSpendModel
    {
        public Guid CostId { get; set; }

        public Guid? Id { get; set; }

        [Required]
        public Guid CostDetailId { get; set; }

        [RegularExpression(@"[\+-]?[0-9]{1,}")]
        public int Value { get; set; }

        public string Comment { get; set; }

        public int Days { get; set; }
    }
}