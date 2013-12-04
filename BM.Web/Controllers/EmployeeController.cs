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
    public class EmployeeController : Controller
    {
        IEmployeeService eService = ServiceFactory.GetEmployeeService();

        public ActionResult ManageEmployee()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageEmployee(Employee model)
        {
            try
            {                
                eService.SaveEmployeeInfo(model);
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
            Employee emp = new Employee();
            if (null != id)
            {
                emp.id = id;
                Employee _emp = this.eService.Load(emp);
                return View(_emp);
            }
            return View(new Employee());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Employee model)
        {
            try
            {
                eService.Update(model);
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
            return View(this.eService.LoadAll());
        }
    }
}
