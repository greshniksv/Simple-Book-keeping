using System;
using SimpleBookKeeping.Database.Entities;

namespace SimpleBookKeeping.Models
{
    public class PlanMemberModel
    {
        public Guid Id { get; set; }

        public UserModel User { get; set; }

        public PlanModel Plan { get; set; }
    }
}