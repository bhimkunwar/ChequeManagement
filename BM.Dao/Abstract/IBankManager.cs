using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;

namespace BM.Dao.Abstract
{
    public interface IBankManager
    {
        void SaveBankInfo(Bank entity);
        IEnumerable<Bank> LoadAll();
        Bank Load(Bank entity);
        void Update(Bank entity);
    }
}
