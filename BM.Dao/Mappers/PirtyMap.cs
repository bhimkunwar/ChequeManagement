using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;
using FluentNHibernate.Mapping;
namespace BM.Dao.Mappers
{
    public class PirtyMap : ClassMap<Pirty>
    {
        public PirtyMap()
        {
            Id(x => x.id).GeneratedBy.Identity();

            Map(x => x.Name);
            Map(x => x.Designation);

            HasMany(x => x.cheque);
            
        }
    }
}
