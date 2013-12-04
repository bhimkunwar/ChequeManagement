using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Core.Entity.Cheque
{
    public class Firm
    {
        public virtual int id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string Address { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Url { get; set; }

        public virtual IList<Pirty> pirty { get; set; }
    }
}
