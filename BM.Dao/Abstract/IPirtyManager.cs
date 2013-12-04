using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;

namespace BM.Dao.Abstract
{
    public interface IPirtyManager
    {
        void SavePirtyInfo(Pirty entity);
        IEnumerable<Pirty> LoadAll();
        Pirty Load(Pirty entity);
        void Update(Pirty entity);
    }
}
