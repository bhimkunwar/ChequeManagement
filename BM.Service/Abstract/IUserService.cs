using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity;

namespace BM.Service.Abstract
{
    public interface IUserService
    {
        User Login(string userName, string password);
        IEnumerable<User> LoadAll();
        User Load(User entity);
        void CreateUser(User entity);
        bool IsPasswordValid(User entity);
    }
}
