using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;

namespace BM.Dao.Abstract
{
    public interface IEmployeeManager
    {
        void SaveEmplInfo(Employee entity);
        IEnumerable<Employee> LoadAll();
        Employee Load(Employee entity);
        void Update(Employee entity);
    }
}
