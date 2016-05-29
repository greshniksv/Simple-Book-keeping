﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleBookKeeping.Models
{
    public class CostModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Имя должно быть указано")]
        [StringLength(maximumLength: 30, ErrorMessage = "Имя не должно превышать 10 символов")]
        public string Name { get; set; }

        public List<CostDetailModel> CostDetails { get; set; }
    }
}