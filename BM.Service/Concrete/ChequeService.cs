using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;
using BM.Dao;
using BM.Dao.Abstract;
using BM.Service.Abstract;

namespace BM.Service.Concrete
{
    public class ChequeService : IChequeService
    {
        IChequeManager cManager = DaoFactory.GetChequeManager();
        public void SaveChequeInfo(Core.Entity.Cheque.Cheque entity)
        {
            cManager.SaveChequeInfo(entity);
        }

        public IEnumerable<Core.Entity.Cheque.Cheque> LoadAll()
        {
            return cManager.LoadAll();
        }

        public Core.Entity.Cheque.Cheque Load(Core.Entity.Cheque.Cheque entity)
        {
            return cManager.Load(entity);
        }

        public void Update(Core.Entity.Cheque.Cheque entity)
        {
            cManager.Update(entity);
        }
    }
}
