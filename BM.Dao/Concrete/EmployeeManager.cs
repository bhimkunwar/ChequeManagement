using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;
using BM.Dao.Abstract;

namespace BM.Dao.Concrete
{
    public class EmployeeManager : BaseDao, IEmployeeManager
    {

        public void SaveEmplInfo(Core.Entity.Cheque.Employee entity)
        {
            base.Save(entity);
        }

        public IEnumerable<Core.Entity.Cheque.Employee> LoadAll()
        {
            return base.LoadAll<Employee>();
        }

        public Core.Entity.Cheque.Employee Load(Core.Entity.Cheque.Employee entity)
        {
            return base.Load<Employee>(entity.id);
        }

        public void Update(Core.Entity.Cheque.Employee entity)
        {
            base.Update(entity);
        }
    }
}
