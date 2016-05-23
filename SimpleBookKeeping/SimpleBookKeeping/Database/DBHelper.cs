using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SimpleBookKeeping.Database.Entities;

namespace SimpleBookKeeping.Database
{
    public class DBHelper
    {
        internal static ISession SessionForTest
        {
            get;
            set;
        }

        #region Private

        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly(typeof(User).Assembly);
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        #endregion

        public static ISession OpenSession()
        {
            return SessionForTest ?? SessionFactory.OpenSession();
        }

        public static void CreateDb()
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(User).Assembly);
            var schemaExport = new SchemaExport(configuration);
            schemaExport.Create(false, true);
        }

    }
}