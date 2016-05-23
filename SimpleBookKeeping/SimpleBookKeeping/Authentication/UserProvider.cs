using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using NHibernate;

namespace SimpleBookKeeping.Authentication
{
    public class UserProvider : IPrincipal
    {
        private UserIndentity UserIdentity { get; set; }

        public IIdentity Identity => UserIdentity;

        public bool IsInRole(string role)
        {
            if (UserIdentity.User == null)
            {
                return false;
            }
            return true;
        }

        public UserProvider(string id, ISession session)
        {
            UserIdentity = new UserIndentity();
            UserIdentity.Init(id, session);
        }

        public override string ToString()
        {
            return UserIdentity.Name;
        }
    }
}