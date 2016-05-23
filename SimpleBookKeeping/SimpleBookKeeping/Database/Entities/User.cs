using System;

namespace SimpleBookKeeping.Database.Entities
{
    public class User
    {
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
        public virtual string Login
        {
            get;
            set;
        }
        public virtual string Password
        {
            get;
            set;
        }
    }
}