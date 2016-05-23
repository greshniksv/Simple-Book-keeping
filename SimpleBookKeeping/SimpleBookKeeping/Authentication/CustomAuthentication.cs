using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using NHibernate.Criterion;
using Ninject;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;

namespace SimpleBookKeeping.Authentication
{
    public class CustomAuthentication : IAuthentication
    {

        private const string cookieName = "__AUTH_COOKIE";

        public HttpContext HttpContext { get; set; }
        
        #region IAuthentication Members

        public User Login(string userName, string password, bool isPersistent)
        {
            var session = DBHelper.OpenSession();
            var criteria = session.CreateCriteria<User>();
            criteria.Add(Restrictions.Eq("Login", userName));
            criteria.Add(Restrictions.Eq("Password", password));

            User retUser = criteria.List<User>().First();
            if (retUser != null)
            {
                CreateCookie(userName, isPersistent);
            }
            return retUser;
        }

        //public User Login(string userName)
        //{
        //    User retUser = Repository.Users.FirstOrDefault(p => string.Compare(p.Email, userName, true) == 0);
        //    if (retUser != null)
        //    {
        //        CreateCookie(userName);
        //    }
        //    return retUser;
        //}

        private void CreateCookie(string userName, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                  1,
                  userName,
                  DateTime.Now,
                  DateTime.Now.Add(FormsAuthentication.Timeout),
                  isPersistent,
                  string.Empty,
                  FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var AuthCookie = new HttpCookie(cookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };
            

            HttpContext.Response.Cookies.Set(AuthCookie);
        }

        public void LogOut()
        {
            var httpCookie = HttpContext.Response.Cookies[cookieName];
            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }
        }

        private IPrincipal _currentUser;

        public IPrincipal CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    try
                    {
                        HttpCookie authCookie = HttpContext.Request.Cookies.Get(cookieName);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            _currentUser = new UserProvider(ticket.Name, DBHelper.OpenSession());
                        }
                        else
                        {
                            _currentUser = new UserProvider(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        _currentUser = new UserProvider(null, null);
                    }
                }
                return _currentUser;
            }
        }
        #endregion
    }
}