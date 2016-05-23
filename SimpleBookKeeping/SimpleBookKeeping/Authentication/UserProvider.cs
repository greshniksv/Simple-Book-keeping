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
        private UserIndentity userIdentity { get; set; }

        #region IPrincipal Members

        public IIdentity Identity
        {
            get
            {
                return userIdentity;
            }
        }

        public bool IsInRole(string role)
        {
            if (userIdentity.User == null)
            {
                return false;
            }
            return true;
        }

        #endregion


        public UserProvider(string name, ISession session)
        {
            userIdentity = new UserIndentity();
            userIdentity.Init(name, session);
        }

        public override string ToString()
        {
            return userIdentity.Name;
        }
    }
}