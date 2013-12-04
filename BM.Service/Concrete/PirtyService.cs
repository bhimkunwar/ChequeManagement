using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Dao;
using BM.Dao.Abstract;
using BM.Service.Abstract;

namespace BM.Service.Concrete
{
    public class PirtyService : IPirtyService
    {
        IPirtyManager pManager = DaoFactory.GetPirtyManager();
        public void SavePirtyInfo(Core.Entity.Cheque.Pirty entity)
        {
            pManager.SavePirtyInfo(entity);
        }

        public IEnumerable<Core.Entity.Cheque.Pirty> LoadAll()
        {
            return pManager.LoadAll();
        }

        public Core.Entity.Cheque.Pirty Load(Core.Entity.Cheque.Pirty entity)
        {
            return pManager.Load(entity);
        }

        public void Update(Core.Entity.Cheque.Pirty entity)
        {
            pManager.Update(entity);
        }
    }
}
