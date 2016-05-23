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
using SimpleBookKeeping.Exceptions;

namespace SimpleBookKeeping.Authentication
{
    public class CustomAuthentication : IAuthentication
    {
        private const string CookieName = "__AUTH_COOKIE";

        public HttpContext HttpContext { get; set; }
        
        
        public User Login(string userName, string password, bool isPersistent)
        {
            var session = DBHelper.OpenSession();
            var criteria = session.CreateCriteria<User>();
            criteria.Add(Restrictions.Eq("Login", userName));
            criteria.Add(Restrictions.Eq("Password", password));

            User retUser = criteria.List<User>().FirstOrDefault();
            if (retUser != null)
            {
                CreateCookie(retUser.Id.ToString(), isPersistent);
            }
            return retUser;
        }

        private void CreateCookie(string userId, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                  1,
                  userId,
                  DateTime.Now,
                  DateTime.Now.Add(FormsAuthentication.Timeout),
                  isPersistent,
                  string.Empty,
                  FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var authCookie = new HttpCookie(CookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };

            HttpContext.Response.Cookies.Set(authCookie);
        }

        public void LogOut()
        {
            var httpCookie = HttpContext.Response.Cookies[CookieName];
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
                        HttpCookie authCookie = HttpContext.Request.Cookies.Get(CookieName);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            if (ticket == null)
                            {
                                throw new CookieDecryptException($"Cookie value: {authCookie.Value}");
                            }
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
        
    }
}