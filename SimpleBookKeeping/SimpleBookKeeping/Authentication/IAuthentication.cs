
using System.Security.Principal;
using System.Web;
using SimpleBookKeeping.Database.Entities;

namespace SimpleBookKeeping.Authentication
{
    public interface IAuthentication
    {
        HttpContext HttpContext { get; set; }

        User Login(string login, string password, bool isPersistent);
        
        void LogOut();

        IPrincipal CurrentUser { get; }
    }
}