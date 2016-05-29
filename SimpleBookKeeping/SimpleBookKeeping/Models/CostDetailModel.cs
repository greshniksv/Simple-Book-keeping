using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleBookKeeping.Models
{
    public class CostDetailModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Дата должна быть указана")]
        public DateTime Date { get; set; }

        public int Value { get; set; }
    }
}