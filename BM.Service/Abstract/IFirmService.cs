using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;

namespace BM.Service.Abstract
{
    public interface IFirmService
    {
        void SaveFirmInfo(Firm entity);
        IEnumerable<Firm> LoadAll();
        Firm Load(Firm entity);
        void Update(Firm entity);
    }
}
