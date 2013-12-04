using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;
using FluentNHibernate.Mapping;
namespace BM.Dao.Mappers
{
    public class FirmMap : ClassMap<Firm>
    {
        public FirmMap()
        {
            Id(x => x.id).GeneratedBy.Identity();

            Map(x => x.Name);
            Map(x => x.Type);
            Map(x => x.Url);
            Map(x => x.ContactPerson);
            Map(x => x.EmailAddress);

            HasMany(x => x.pirty);
        }
    }
}
