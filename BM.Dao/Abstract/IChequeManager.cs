using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;

namespace BM.Dao.Abstract
{
    public interface IChequeManager
    {
        void SaveChequeInfo(Cheque entity);
        IEnumerable<Cheque> LoadAll();
        Cheque Load(Cheque entity);
        void Update(Cheque entity);
    }
}
