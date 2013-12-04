using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;

namespace BM.Service.Abstract
{
    public interface IEmployeeService
    {
        void SaveEmployeeInfo(Employee entity);
        IEnumerable<Employee> LoadAll();
        Employee Load(Employee entity);
        void Update(Employee entity);
    }
}
