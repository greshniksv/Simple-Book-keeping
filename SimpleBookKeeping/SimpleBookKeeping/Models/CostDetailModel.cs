using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SimpleBookKeeping.Models
{
    public class CostDetailModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Дата должна быть указана")]
        public DateTime Date { get; set; }

        [Range(0, 50000, ErrorMessage = "Расход может быть от 0 до 50 000")]
        public int? Value { get; set; }
    }
}