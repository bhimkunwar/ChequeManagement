using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity;
using BM.Core.Entity.Cheque;
using BM.Dao;
using BM.Dao.Abstract;
using BM.Service.Abstract;

namespace BM.Service.Concrete
{
    public class BankService : IBankService
    {
        IBankManager bManager = DaoFactory.GetBankManager();
        public void SaveBankInfo(Core.Entity.Cheque.Bank entity)
        {
            bManager.SaveBankInfo(entity);
        }

        public IEnumerable<Core.Entity.Cheque.Bank> LoadAll()
        {
            return bManager.LoadAll();
        }

        public Core.Entity.Cheque.Bank Load(Core.Entity.Cheque.Bank entity)
        {
            return bManager.Load(entity);
        }

        public void Update(Core.Entity.Cheque.Bank entity)
        {
            Bank user = new Bank();
            bManager.Update(entity);
        }
    }
}
