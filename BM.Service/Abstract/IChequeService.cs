using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;

namespace BM.Service.Abstract
{
    public interface IChequeService
    {
        void SaveChequeInfo(Cheque entity);
        IEnumerable<Cheque> LoadAll();
        Cheque Load(Cheque entity);
        void Update(Cheque entity);
    }
}
