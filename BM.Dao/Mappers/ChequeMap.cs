using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
namespace BM.Dao.Mappers
{
    public class ChequeMap : ClassMap<Cheque>
    {
        public ChequeMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.BankName);
            Map(x => x.BearerName);
            Map(x => x.ChequeNumber);
            Map(x => x.ChqType);
            Map(x => x.FirmName);
            Map(x => x.IssuedDate);
            Map(x => x.PaidAmount);
            Map(x => x.PaymentDate);
            Map(x => x.PaymentType);
            Map(x => x.PirtyName);


        }
    }
}
