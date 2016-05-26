using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.DatabaseConfig.Entities.Interfaces;

namespace SimpleBookKeeping.DatabaseConfig
{
    public class Database<T>
    {
        private ISession session;
        private ICriteria criteria;
        
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public Database(bool selectDeleted = false)
        {
            session = DBHelper.OpenSession();
            //using (ITransaction transaction = session.BeginTransaction())
            //{
            //    ICriteria criteria = session.CreateCriteria(typeof(User));
            //    criteria.Add(Restrictions.Eq("Id", "efd174aa-fd61-40f4-ae37-ea5fe1cc0f6d"));
            //    IList<User> matchingObjects = criteria.List<User>();
            //    transaction.Commit();
            //    var user = matchingObjects.First();
            //}

            criteria = session.CreateCriteria(typeof(T));
            if (!selectDeleted && typeof(T).IsSubclassOf(typeof(IDeleteMarker)))
            {
                criteria.Add(Restrictions.Eq("Deleted", false));
            }
        }

        public Database<T> AddEq(string column, object value)
        {
            criteria.Add(Restrictions.Eq(column, value));
            return this;
        }

        public IList<T> Select<T>()
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                var matchingObjects = criteria.List<T>();
                transaction.Commit();
                return matchingObjects;
            }
        }

        public bool Insert<T>(T item)
        {
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(item);
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                return false; 
            }
            return true;
        }

        public bool Insert<T>(IEnumerable<T> items)
        {
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    foreach (var item in items)
                    {
                        session.Save(item);
                    }
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update<T>(T item)
        {
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(item);
                    transaction.Commit();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public bool Update<T>(IEnumerable<T> items)
        {
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    foreach (var item in items)
                    {
                        session.Update(item);
                    }
                    transaction.Commit();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }


        public bool Delete<T>(T item)
        {
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    if (item is IDeleteMarker)
                    {
                        ((IDeleteMarker) item).Deleted = true;
                        session.Update(item);
                    }
                    else
                    {
                        session.Delete(item);
                    }
                    transaction.Commit();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public bool Delete<T>(IEnumerable<T> items)
        {
            if (!items.Any())
            {
                return false;
            }

            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    if (items.First() is IDeleteMarker)
                    {
                        foreach (var item in items)
                        {
                            ((IDeleteMarker)item).Deleted = true;
                            session.Update(item);
                        }
                    }
                    else
                    {
                        foreach (var item in items)
                        {
                            session.Delete(item);
                        }
                    }
                    transaction.Commit();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

    }
}