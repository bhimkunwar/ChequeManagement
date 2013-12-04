using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Core.Entity.Cheque
{
    public class Employee
    {
        public virtual int id { get; set; }
        public virtual string Name { get; set; }
        public virtual string surnanme { get; set; }
        public virtual string Address { get; set; }
        public virtual string Gender { get; set; }
        public virtual DateTime? JoinDate { get; set; }
        public virtual DateTime? Dob { get; set; }
        public virtual string EmployeementType { get; set; }
    }
}
