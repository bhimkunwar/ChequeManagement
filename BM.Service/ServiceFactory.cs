using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BM.Core;
using BM.Core.Entity;
using BM.Service.Abstract;
using BM.Service.Concrete;
using BM.Dao;

namespace BM.Service
{
    public static class ServiceFactory
    {
        public static IUserService GetUserService()
        {
            return new UserService();
        }

        public static IBankService GetBankService()
        {
            return new BankService();
        }

        public static IFirmService GetFirmService()
        {
            return new FirmService();
        }

        public static IPirtyService GetPirtyService()
        {
            return new PirtyService();
        }

        public static IChequeService GetChequeService()
        {
            return new ChequeService();
        }

        public static IEmployeeService GetEmployeeService()
        {
            return new EmployeeService();
        }
    }
}
