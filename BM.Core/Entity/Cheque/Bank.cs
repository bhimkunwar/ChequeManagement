using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Core.Entity.Cheque
{
    public class Bank
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string BankName { get; set; }
        public virtual string BankAddress { get; set; }

        public virtual IList<Cheque> cheque { get; set; }
    }
}
