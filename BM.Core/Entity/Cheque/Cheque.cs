using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Core.Entity.Cheque
{
    public class Cheque
    {
        public virtual int Id { get; set; }
        public virtual string ChequeNumber { get; set; }
        public virtual string FirmName { get; set; }
        public virtual string PirtyName { get; set; }
        public virtual string BankName { get; set; }
        public virtual string BearerName { get; set; }
        public virtual double PaidAmount { get; set; }
        public virtual string ChqType { get; set; }
        public virtual string PaymentType { get; set; }
        public virtual string PaymentDate { get; set; }
        public virtual string IssuedDate { get; set; }

        public virtual Bank bank { get; set; }
        public virtual Pirty pirty { get; set; }
    }
}
