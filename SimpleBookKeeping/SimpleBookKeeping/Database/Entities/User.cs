using System;
using System.Collections.Generic;

namespace SimpleBookKeeping.Database.Entities
{
    public class User
    {
        IList<Plan> _palns = new List<Plan>();
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

        public virtual IList<Plan> Plans
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