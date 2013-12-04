using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity;
using BM.Dao.Abstract;
using NHibernate;
namespace BM.Dao.Concrete
{
    public class UserManager : BaseDao, IUserManager
    {

        public User Login(string userName, string password)
        {
            return base.Get<User>("loginName", userName);
        }

        public IEnumerable<User> LoadAll()
        {
            return base.LoadAll<User>();
        }

        public User Load(User entity)
        {
            return base.Load<User>(entity.id);
        }

        public void Save(User entity)   
        {
            base.Save(entity);
        }

        public object SaveandReturn(User entity)
        {
            object _user ;
            ISession session = null;
            session = NhibernateHelper.OpenSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                _user = session.Save(entity);
                tx.Commit();
            }
            return _user;
        }
    }
}
