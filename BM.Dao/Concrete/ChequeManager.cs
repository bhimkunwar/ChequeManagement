using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;
using BM.Dao.Abstract;

namespace BM.Dao.Concrete
{
    public class ChequeManager : BaseDao, IChequeManager
    {

        public void SaveChequeInfo(Core.Entity.Cheque.Cheque entity)
        {
            base.Save(entity);
        }

        public IEnumerable<Core.Entity.Cheque.Cheque> LoadAll()
        {
            return base.LoadAll<Cheque>();            
        }

        public Core.Entity.Cheque.Cheque Load(Core.Entity.Cheque.Cheque entity)
        {
            return base.Load<Cheque>(entity.Id);
        }

        public void Update(Core.Entity.Cheque.Cheque entity)
        {
            base.Update(entity);
        }
    }
}
