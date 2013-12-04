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
    public class PirtyController : Controller
    {
        IPirtyService pService = ServiceFactory.GetPirtyService();

        public ActionResult ManagePirty()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManagePirty(Pirty model)
        {
            try
            {
                pService.SavePirtyInfo(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException);
            }
            ModelState.Clear();
            return View();
        }

        public ActionResult view(int id)
        {
            Pirty pirty = new Pirty();
            if (null != id)
            {
                pirty.id = id;
                Pirty _pirty = this.pService.Load(pirty);
                return View(_pirty);
            }
            return View(new Pirty());
        }

        public ActionResult Edit(int id)
        {
            Pirty pirty = new Pirty();
            if (null != id)
            {
                pirty.id = id;
                Pirty _pirty = this.pService.Load(pirty);
                return View(_pirty);
            }
            return View(new Bank());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Pirty model)
        {
            try
            {
                pService.Update(model);
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
            return View(this.pService.LoadAll());
        }

    }
}
