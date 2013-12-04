using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;

namespace BM.Dao.Abstract
{
    public interface IFirmManager
    {
        void SaveFirmInfo(Firm entity);
        IEnumerable<Firm> LoadAll();
        Firm Load(Firm entity);
        void Update(Firm entity);
    }
}
