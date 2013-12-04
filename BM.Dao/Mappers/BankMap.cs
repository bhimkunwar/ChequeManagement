using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using BM.Core.Entity.Cheque;
namespace BM.Dao.Mappers
{
    public class BankMap : ClassMap<Bank>
    {
        public BankMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.BankAddress);
            Map(x => x.BankName);
            Map(x => x.Code);

            HasMany(x => x.cheque).LazyLoad();
        }
    }
}
