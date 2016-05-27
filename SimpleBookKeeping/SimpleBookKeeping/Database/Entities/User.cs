using System;
using System.Collections.Generic;

namespace SimpleBookKeeping.Database.Entities
{
    public class User
    {
        IList<CostPlan> _palns = new List<CostPlan>();
        public virtual Guid Id
        {
            get;
            set;
        }
        public virtual string Name
        {
            get;
            set;
        }
        public virtual string Login
        {
            get;
            set;
        }
        public virtual string Password
        {
            get;
            set;
        }

        public virtual IList<CostPlan> CostPlans
        {
            get
            {
                return _palns;
            }

            set
            {
                _palns = value;
            }
        }
    }
}