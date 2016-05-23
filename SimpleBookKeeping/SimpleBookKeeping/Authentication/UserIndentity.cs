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

        public string AuthenticationType => typeof(User).ToString();

        public bool IsAuthenticated => User != null;

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

        public void Init(string id, ISession session)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ICriteria criteria = session.CreateCriteria(typeof(User));
                criteria.Add(Restrictions.Eq("Id", new Guid("id")));
                IList<User> matchingObjects = criteria.List<User>();

                User = matchingObjects.First();
            }
        }
    }
}