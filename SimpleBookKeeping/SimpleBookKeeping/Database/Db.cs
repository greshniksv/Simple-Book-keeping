using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping;
using NHibernate.Tool.hbm2ddl;
using SimpleBookKeeping.Database.Entities;

namespace SimpleBookKeeping.Database
{
    public static class Db
    {
        
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

        public static ISession Session => SessionFactory.OpenSession();


        public static void CreateDb()
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(User).Assembly);
            var schemaExport = new SchemaExport(configuration);
            schemaExport.Drop(false, true);
            schemaExport.Create(false, true);

            InsertTestData();
        }

        private static void InsertTestData()
        {
            var user = new User
            {
                Name = "admin",
                Login = "admin",
                Password = "123456",
            };

            var usertest = new User
            {
                Name = "test",
                Login = "test",
                Password = "123456",
            };


            var costPlan = new Plan
            {
                Name = "Test plan",
                Start = DateTime.Now.AddDays(-10),
                End = DateTime.Now,
                Balance = 1000,
                Deleted = false,
                User = user
            };

            var cost = new Cost
            {
                Name = "Nanny",
                Deleted = false,
                Plan = costPlan
            };

            for (int i = 10; i > 0; i--)
            {
                cost.CostDetails.Add(new CostDetail
                {
                    Deleted = false,
                    Date = DateTime.Now.AddDays(-i),
                    Value = 100,
                    Cost = cost
                });
            }

            costPlan.Costs.Add(cost);
            user.Plans.Add(costPlan);

            using (var session = Session)
            using (ITransaction transaction = session.BeginTransaction())
            {
                var costDetails = new List<CostDetail>();
                for (int i = 10; i > 0; i--)
                {
                    var item = new CostDetail
                    {
                        Deleted = false,
                        Date = DateTime.Now.AddDays(-i),
                        Value = 100,
                        Cost = cost
                    };
                    session.Save(item);
                    costDetails.Add(item);
                }
                
                session.Save(usertest);
                transaction.Commit();
            }

        }
    }
}