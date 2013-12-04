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
    public class FirmService : IFirmService
    {
        IFirmManager firmManager = DaoFactory.GetFirmManager();
        public void SaveFirmInfo(Core.Entity.Cheque.Firm entity)
        {
            firmManager.SaveFirmInfo(entity);
        }

        public IEnumerable<Core.Entity.Cheque.Firm> LoadAll()
        {
            return firmManager.LoadAll();
        }

        public Core.Entity.Cheque.Firm Load(Core.Entity.Cheque.Firm entity)
        {
            return firmManager.Load(entity);
        }

        public void Update(Core.Entity.Cheque.Firm entity)
        {
            firmManager.Update(entity);
        }
    }
}
