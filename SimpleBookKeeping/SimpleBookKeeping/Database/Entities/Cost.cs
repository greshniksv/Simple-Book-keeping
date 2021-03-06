﻿using System;
using System.Collections.Generic;
using SimpleBookKeeping.DatabaseConfig.Entities.Interfaces;

namespace SimpleBookKeeping.Database.Entities
{
    public class Cost : IDeleteMarker
    {
        private Plan _plan;
        IList<CostDetail> _costDetails = new List<CostDetail>();

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

        public virtual Plan Plan
        {
            get { return _plan; }
            set { _plan = value; }
        }

        public virtual IList<CostDetail> CostDetails
        {
            get { return _costDetails; }
            set { _costDetails = value; }
        }

        public virtual bool Deleted
        {
            get;
            set;
        }
    }
}