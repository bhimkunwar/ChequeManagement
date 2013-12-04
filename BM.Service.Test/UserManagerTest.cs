using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BM.Core.Entity;
using BM.Dao.Abstract;
using BM.Dao;
namespace BM.Service.Test
{
    [TestClass]
    public class UserManagerTest
    {
        //[TestMethod]
        public void CreateUser()
        {
            IUserManager uManager = DaoFactory.GetUserMaanger();
            User _user = new User();

            _user.IsActive = true;
            _user.loginName = "bhim";
            _user.loginPassword = "bhimkunwar";
            _user.Role = "admin";
            uManager.SaveandReturn(_user);
        }   
    }
}
