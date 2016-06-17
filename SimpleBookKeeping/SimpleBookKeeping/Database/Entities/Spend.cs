
using System;

namespace SimpleBookKeeping.Database.Entities
{
    public class Spend
    {
        private User _user;
        private Cost _cost;

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

        public virtual Cost Cost
        {
            get { return _cost; }
            set { _cost = value; }
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

        public virtual string Comment
        {
            get;
            set;
        }
    }
}