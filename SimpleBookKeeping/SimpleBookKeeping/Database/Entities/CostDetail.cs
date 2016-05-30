using System;
using SimpleBookKeeping.DatabaseConfig.Entities.Interfaces;

namespace SimpleBookKeeping.Database.Entities
{
    public class CostDetail: IDeleteMarker
    {
        private Cost _cost;

        public virtual Guid Id
        {
            get;
            set;
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

        public virtual Cost Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        public virtual bool Deleted
        {
            get;
            set;
        }
    }
}