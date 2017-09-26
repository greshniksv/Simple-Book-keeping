
using System;

namespace SimpleBookKeeping.Database.Entities
{
    public class Spend
    {
        private User _user;
        private CostDetail _costDetail;

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

        public virtual CostDetail CostDetail
        {
            get { return _costDetail; }
            set { _costDetail = value; }
        }

        public virtual int OrderId
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

        public virtual string Image
        {
            get;
            set;
        }
    }
}