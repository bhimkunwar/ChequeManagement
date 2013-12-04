using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM.Core.Entity;
using BM.Dao.Abstract;
using BM.Dao.Concrete;
namespace BM.Dao
{
    public static class DaoFactory
    {
        public static IUserManager GetUserMaanger()
        {
            return new UserManager();
        }

        public static IBankManager GetBankManager()
        {
            return new BankManager();
        }
        public static IFirmManager GetFirmManager()
        {
            return new FirmManager();
        }

        public static IEmployeeManager GetEmployeeManager()
        {
            return new EmployeeManager();
        }

        public static IPirtyManager GetPirtyManager()
        {
            return new PirtyManager();
        }

        public static IChequeManager GetChequeManager()
        {
            return new ChequeManager();
        }
    }
}
