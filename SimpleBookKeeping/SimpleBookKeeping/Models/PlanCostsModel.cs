﻿using System;
using System.Collections.Generic;
using SimpleBookKeeping.Database.Entities;

namespace SimpleBookKeeping.Models
{
    public class PlanCostsModel
    {
        public Guid Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public IList<Guid> UserMembers { get; set; }

        public IList<Cost> Costs { get; set; }
    }
}