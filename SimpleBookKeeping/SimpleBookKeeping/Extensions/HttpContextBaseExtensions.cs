using System;
using System.Security.Principal;
using System.Web;
using SimpleBookKeeping.Authentication;

namespace SimpleBookKeeping.Extensions
{
    public static class HttpContextBaseExtensions
    {
        public static Guid UserId(this HttpContextBase httpContext)
        {
            return GetUserId(httpContext.User);
        }

        public static Guid UserId(this HttpContext httpContext)
        {
            return GetUserId(httpContext.User);
        }

        private static Guid GetUserId(IPrincipal principal)
        {
            Guid id = Guid.Empty;
            try
            {
                id = ((UserIndentity)principal.Identity).Id;
            }
            catch (Exception)
            {
                // ignored
            }
            return id;
        }
    }
}