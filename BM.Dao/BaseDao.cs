using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NHibernate;
using NHibernate.Criterion;
namespace BM.Dao
{
    public class BaseDao
    {        

        [DebuggerStepThrough]
        protected virtual ICollection<T> LoadAll<T>() where T : class
        {
            ISession session = null;
            session = NhibernateHelper.OpenSession();
            return session.CreateCriteria<T>().List<T>();
        }

        [DebuggerStepThrough]
        protected T Get<T>(string propertyName, object propertyValue)
        {
            ISession session = null;
            session = NhibernateHelper.OpenSession();
            return session.CreateCriteria(typeof(T))
                .Add(Restrictions.Eq(propertyName,
                                     propertyValue).IgnoreCase()).UniqueResult<T>();
        }

        [DebuggerStepThrough]
        public T Load<T>(int id)
        {
            ISession session = null;
            session = NhibernateHelper.OpenSession();
            return session.Load<T>(id);
        }

        [DebuggerStepThrough]
        public void Save(object entity)
        {
            string test = string.Empty;
            ISession session = null;
            session = NhibernateHelper.OpenSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                session.Save(entity);
                tx.Commit();
            }
        }

        [DebuggerStepThrough]
        public void Update(object entity)
        {
            string test = string.Empty;
            ISession session = null;
            session = NhibernateHelper.OpenSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                session.Update(entity);
                tx.Commit();
            }
        }

        [DebuggerStepThrough]
        public virtual void Delete(object entity)
        {
            ISession session = null;
            session = NhibernateHelper.OpenSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                session.Delete(entity);
                tx.Commit();
            }
        }

        [DebuggerStepThrough]
        public virtual T Get<T>(int id)
        {
            ISession session = null;
            session = NhibernateHelper.OpenSession();
            return session.Get<T>(id);
        }

    }
}
