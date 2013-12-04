using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;
using FluentNHibernate.Mapping;

namespace BM.Dao.Mappers
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Id(x => x.id).GeneratedBy.Identity();

            Map(x => x.Name);
            Map(x => x.surnanme);
            Map(x => x.Gender);
            Map(x => x.JoinDate);
            Map(x => x.EmployeementType);
            Map(x => x.Dob);
        }
    }
}
