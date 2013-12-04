using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity;
using BM.Service.Abstract;
using BM.Dao;
using BM.Dao.Abstract;
namespace BM.Service.Concrete
{
    public class UserService : IUserService
    {
        IUserManager uManager = DaoFactory.GetUserMaanger();
        public User Login(string userName, string password)
        {
            User user = new User();
            user = uManager.Login(userName, password);
            if (user != null)
                return user;
            else
                return null;
        }

        public IEnumerable<User> LoadAll()
        {
            return uManager.LoadAll();
        }

        public User Load(User entity)
        {
            return uManager.Load(entity);
        }

        public bool IsPasswordValid(User entity)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(User entity)
        {
            User user = new User();
            uManager.Save(entity);
        }
    }
}
