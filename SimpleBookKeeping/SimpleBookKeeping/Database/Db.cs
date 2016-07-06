using System;
using System.Collections.Generic;
using System.Linq;
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
            Random random = new Random();

            var user = new User
            {
                Name = "admin",
                Login = "admin",
                Password = "123456",
            };

            var lenkaUser = new User
            {
                Name = "Ленка",
                Login = "lenka",
                Password = "123456",
            };

            var sergUser = new User
            {
                Name = "Серж",
                Login = "serg",
                Password = "123456",
            };

            var lenkaPlan = new Plan
            {
                Name = "Ленка план",
                Start = DateTime.Now.AddDays(-10),
                End = DateTime.Now.AddDays(10),
                Balance = 2000,
                Deleted = false,
                User = sergUser
            };

            var lenkaCost = new Cost
            {
                Name = "Косметика",
                Deleted = false,
                Plan = lenkaPlan
            };

            var sergPlan = new Plan
            {
                Name = "Сержо",
                Start = DateTime.Now.AddDays(-10),
                End = DateTime.Now.AddDays(10),
                Balance = 3000,
                Deleted = false,
                User = sergUser
            };

            var sergCost = new Cost
            {
                Name = "Игрухи",
                Deleted = false,
                Plan = sergPlan
            };
            
            var plan = new Plan
            {
                Name = "Общий",
                Start = DateTime.Now.AddDays(-10),
                End = DateTime.Now.AddDays(10),
                Balance = 4000,
                Deleted = false,
                User = user
            };

            var cost = new Cost
            {
                Name = "Nanny",
                Deleted = false,
                Plan = plan
            };
            
            plan.Costs.Add(cost);
            user.Plans.Add(plan);

            sergPlan.Costs.Add(sergCost);
            sergUser.Plans.Add(sergPlan);

            lenkaPlan.Costs.Add(lenkaCost);
            lenkaUser.Plans.Add(lenkaPlan);

            PlanMember planMember1 = new PlanMember
            {
                Plan = plan,
                User = sergUser
            };

            PlanMember planMember2 = new PlanMember
            {
                Plan = plan,
                User = lenkaUser
            };

            using (var session = Session)
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(user);
                session.Save(sergUser);
                session.Save(lenkaUser);

                session.Save(sergPlan);
                session.Save(sergCost);

                session.Save(lenkaPlan);
                session.Save(lenkaCost);
                
                session.Save(plan);
                session.Save(cost);

                session.Save(planMember1);
                session.Save(planMember2);

                var sergCostDetails = new List<CostDetail>();
                var start = cost.Plan.Start;
                var end = cost.Plan.End;

                for (DateTime i = start; i < end; i = i.AddDays(1))
                {
                    var item = new CostDetail
                    {
                        Deleted = false,
                        Date = i,
                        Value = 300,
                        Cost = sergCost
                    };
                    session.Save(item);

                    var spend = new Spend
                    {
                        User = sergUser,
                        CostDetail = item,
                        Value = random.Next(1, 500)
                    };
                    session.Save(spend);

                    sergCostDetails.Add(item);
                }


                var costDetails = new List<CostDetail>();
               
                for (DateTime i = start; i < end; i = i.AddDays(1))
                {
                    var item = new CostDetail
                    {
                        Deleted = false,
                        Date = i,
                        Value = 200,
                        Cost = cost
                    };
                    session.Save(item);

                    var spend = new Spend
                    {
                        User = user,
                        CostDetail = item,
                        Value = random.Next(1,500)
                    };
                    session.Save(spend);

                    costDetails.Add(item);
                }

                transaction.Commit();
            }
        }
    }
}