using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;

namespace BM.Service.Abstract
{
    public interface IBankService
    {
        void SaveBankInfo(Bank entity);
        IEnumerable<Bank> LoadAll();
        Bank Load(Bank entity);
        void Update(Bank entity);
    }
}
    