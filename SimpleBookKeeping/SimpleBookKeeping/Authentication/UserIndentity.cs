using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using NHibernate;
using NHibernate.Criterion;
using SimpleBookKeeping.Database.Entities;

namespace SimpleBookKeeping.Authentication
{
    public class UserIndentity : IIdentity
    {
        public User User { get; set; }

        public string AuthenticationType
        {
            get
            {
                return typeof(User).ToString();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }

        public string Name
        {
            get
            {
                if (User != null)
                {
                    return User.Name;
                }
                //иначе аноним
                return "anonym";
            }
        }

        public void Init(string name, ISession session)
        {
            if (!string.IsNullOrEmpty(name))
            {
                ICriteria criteria = session.CreateCriteria(typeof(User));
                criteria.Add(Restrictions.Eq("Name", name));
                IList<User> matchingObjects = criteria.List<User>();

                User = matchingObjects.First();
            }
        }
    }
}