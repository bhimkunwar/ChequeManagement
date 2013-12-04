using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;
using BM.Dao.Abstract;

namespace BM.Dao.Concrete
{
    public class FirmManager : BaseDao , IFirmManager
    {
        public void SaveFirmInfo(Core.Entity.Cheque.Firm entity)
        {
            base.Save(entity);
        }

        public IEnumerable<Core.Entity.Cheque.Firm> LoadAll()
        {
            return base.LoadAll<Firm>();
        }

        public Core.Entity.Cheque.Firm Load(Core.Entity.Cheque.Firm entity)
        {
            return base.Load<Firm>(entity.id);
        }

        public void Update(Core.Entity.Cheque.Firm entity)
        {
            base.Update(entity);
        }
    }
}
