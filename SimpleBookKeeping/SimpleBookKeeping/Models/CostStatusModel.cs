using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBookKeeping.Models
{
    public class CostStatusModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }
    }
}