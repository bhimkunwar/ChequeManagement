using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
namespace BM.Dao.Mappers
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.id).GeneratedBy.Identity();

            Map(x => x.loginName);
            Map(x => x.loginPassword);
            Map(x => x.Role);
            Map(x => x.IsActive);
        }
    }
}
