using System;
using System.Collections.Generic;
using SimpleBookKeeping.DatabaseConfig.Entities.Interfaces;

namespace SimpleBookKeeping.Database.Entities
{
    public class Plan : IDeleteMarker
    {
        private User _user;
        IList<Cost> _costs = new List<Cost>();

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

        public virtual DateTime Start
        {
            get;
            set;
        }

        public virtual DateTime End
        {
            get;
            set;
        }

        public virtual int Balance
        {
            get;
            set;
        }

        public virtual IList<Cost> Costs
        {
            get
            {
                return _costs;
            }

            set
            {
                _costs = value;
            }
        }

        public virtual User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public virtual bool Deleted
        {
            get;
            set;
        }
    }
}