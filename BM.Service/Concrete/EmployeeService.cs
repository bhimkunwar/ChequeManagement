using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;
using BM.Dao;
using BM.Dao.Abstract;
using BM.Service.Abstract;

namespace BM.Service.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeManager eManager = DaoFactory.GetEmployeeManager();
        public void SaveEmployeeInfo(Core.Entity.Cheque.Employee entity)
        {
            eManager.SaveEmplInfo(entity);
        }

        public IEnumerable<Core.Entity.Cheque.Employee> LoadAll()
        {
            return eManager.LoadAll();
        }

        public Core.Entity.Cheque.Employee Load(Core.Entity.Cheque.Employee entity)
        {
            return eManager.Load(entity);
        }

        public void Update(Core.Entity.Cheque.Employee entity)
        {
            Employee employee = new Employee();
            eManager.Update(entity);
        }
    }
}
