using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity.Cheque;
using BM.Dao.Abstract;

namespace BM.Dao.Concrete
{
    public class PirtyManager : BaseDao, IPirtyManager
    {

        public void SavePirtyInfo(Core.Entity.Cheque.Pirty entity)
        {
            base.Save(entity);
        }

        public IEnumerable<Core.Entity.Cheque.Pirty> LoadAll()
        {
            return base.LoadAll<Pirty>();
        }

        public Core.Entity.Cheque.Pirty Load(Core.Entity.Cheque.Pirty entity)
        {
            return base.Load<Pirty>(entity.id);
        }

        public void Update(Core.Entity.Cheque.Pirty entity)
        {
            base.Update(entity);
        }
    }
}
