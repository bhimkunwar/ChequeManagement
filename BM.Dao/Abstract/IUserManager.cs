using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity;

namespace BM.Dao.Abstract
{
    public interface IUserManager 
    {
        User Login(string userName, string password);
        IEnumerable<User> LoadAll();
        User Load(User entity);
        void Save(User entity);
        object SaveandReturn(User entity);
    }
}
