
using System;

namespace SimpleBookKeeping.Database.Entities
{
    public class Expenditure
    {
        private User _user;
        private Plan _plan;

        public virtual Guid Id
        {
            get;
            set;
        }
        
        public virtual User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public virtual Plan Plan
        {
            get { return _plan; }
            set { _plan = value; }
        }

        public virtual DateTime Date
        {
            get;
            set;
        }

        public virtual int Value
        {
            get;
            set;
        }
    }
}