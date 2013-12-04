using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Core.Entity.Cheque
{
    public class Pirty
    {
        public virtual int id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Designation { get; set; }

        public virtual Firm firm { get; set; }
        public virtual IList<Cheque> cheque { get; set; }
    }
}
