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
    public class ChequeController : Controller
    {

        IChequeService uService = ServiceFactory.GetChequeService();

        public ActionResult ManageCheque()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageCheque(Cheque model)
        {
            try
            {
                uService.SaveChequeInfo(model);
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
            Cheque cheque = new Cheque();
            if (null != id)
            {
                cheque.Id = id;
                Cheque _cheque = this.uService.Load(cheque);
                return View(_cheque);
            }
            return View(new Bank());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Cheque model)
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
            return View(this.uService.LoadAll());
        }

    }
}
