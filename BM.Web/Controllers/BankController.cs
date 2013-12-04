using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BM.Core.Entity.Cheque;
using BM.Service;
using BM.Service.Abstract;

namespace BM.Web.Controllers
{
    public class BankController : Controller
    {

        IBankService uService = ServiceFactory.GetBankService();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManageBank()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageBank(Bank model)
        {
            try
            {
                uService.SaveBankInfo(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException);
            }
            ModelState.Clear();
            return View();
        }        

        public ActionResult Edit(int id)
        {
            Bank bank = new Bank();
            if (null != id)
            {
                bank.Id = id;
                Bank _bank = this.uService.Load(bank);
                return View(_bank);
            }
            return View (new Bank());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Bank model)
        {
            try
            {
                uService.Update(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException);
            }
            ModelState.Clear();
            return View(); 
        }

        public ActionResult List()
        {            
            //return Json(this.uService.LoadAll(), "json", JsonRequestBehavior.AllowGet);
            return View(this.uService.LoadAll());
        }
     
    }
}
