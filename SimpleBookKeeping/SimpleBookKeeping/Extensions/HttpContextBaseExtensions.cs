using System;
using System.Web;
using SimpleBookKeeping.Authentication;

namespace SimpleBookKeeping.Extensions
{
    public static class HttpContextBaseExtensions
    {
        public static Guid UserId(this HttpContextBase httpContext)
        {
            return ((UserIndentity)httpContext.User.Identity).Id;
        }

        public static Guid UserId(this HttpContext httpContext)
        {
            return ((UserIndentity)httpContext.User.Identity).Id;
        }
    }
}