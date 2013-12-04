using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BM.Service;
using BM.Service.Abstract;
using BM.Core.Entity;
namespace BM.Web.Controllers
{
    public class UserController : Controller
    {
        IUserService uService = ServiceFactory.GetUserService();
        public ActionResult LogOn()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LogOn(User entity)
        {
            if (!this.IsCredentialValid(entity.loginName, entity.loginPassword))
            {
                return View();
            }

            return RedirectToAction("Dashboard");

            //return RedirectToAction("Index",
              //          "Dashboard", new { area = "Admin" });
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        private void InvalidLogin(string message)
        {
            TempData["loginValidation"] = message;
        }
        private bool IsCredentialValid(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                this.InvalidLogin("username can't be empty.");
                return false;
            }
            if (String.IsNullOrEmpty(password))
            {
                this.InvalidLogin("password can't be empty.");
                return false;
            }
            if (!this.IsPasswordValid(userName, password))
            {

            }
            return true;
        }
        private bool IsPasswordValid(string userName, string password)
        {
            BM.Core.Entity.User user = uService.Login(userName, password);
            if (null != user)
                return true;
            return false;
        }

        public ActionResult RegisterUser()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegisterUser(User entity)
        {
            uService.CreateUser(entity);
            ModelState.Clear();
            return View();
        }
    }
}
