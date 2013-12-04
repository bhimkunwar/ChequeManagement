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
    public class FirmController : Controller
    {
        IFirmService fService = ServiceFactory.GetFirmService();

        public ActionResult ManageFirm()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageFirm(Firm model)
        {
            try
            {
                fService.SaveFirmInfo(model);
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
            Firm firm = new Firm();
            if (null != id)
            {
                firm.id = id;
                Firm _firm = this.fService.Load(firm);
                return View(_firm);
            }
            return View(new Firm());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Firm model)
        {
            try
            {
                fService.Update(model);
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
            return View(this.fService.LoadAll());
        }

        public List<string> ListFirm()
        {
            List<string> item = new List<string>();
            item.Add("Franklin distilled water");
            item.Add("Safa Paani");
            item.Add("Peune Paani");
            item.Add("Khane Paani");
            return item;
        }

    }
}
