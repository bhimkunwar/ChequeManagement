using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;
using BM.Dao.Abstract;

namespace BM.Dao.Concrete
{
    public class BankManager : BaseDao, IBankManager
    {
        public void SaveBankInfo(Bank entity)
        {
            base.Save(entity);
        }

        public IEnumerable<Bank> LoadAll()
        {
            return base.LoadAll<Bank>();
        }

        public Bank Load(Bank entity)
        {
            return base.Load<Bank>(entity.Id);
        }

        public void Update(Bank entity)
        {
            base.Update(entity);
        }
    }
}
