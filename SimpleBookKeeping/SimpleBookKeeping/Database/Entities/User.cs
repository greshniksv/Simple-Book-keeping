using System;
using System.Collections.Generic;

namespace SimpleBookKeeping.Database.Entities
{
    public class User
    {
        IList<Spend> _spend = new List<Spend>();
        IList<Plan> _palns = new List<Plan>();
        IList<PlanMember> _palnMembers = new List<PlanMember>();

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

        public virtual bool IsAdmin
        {
            get;
            set;
        }

        public virtual IList<Plan> Plans
        {
            get { return _palns; }
            set { _palns = value; }
        }

        public virtual IList<Spend> Spends
        {
            get { return _spend; }
            set { _spend = value; }
        }

        public virtual IList<PlanMember> PlanMembers
        {
            get { return _palnMembers; }
            set { _palnMembers = value; }
        }
    }
}