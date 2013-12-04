using System;
using BM.Core.Entity.Cheque;
using BM.Dao.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BM.Dao.Test
{
    [TestClass]
    public class FirmManagerTest
    {
        [TestMethod]
        public void SaveFirmManager()
        {
            IFirmManager fManager = DaoFactory.GetFirmManager();
            Firm firm = new Firm();

            firm.EmailAddress = "kunwar_7@yahoo.com";
            firm.ContactPerson = "bhim kunwar";
            firm.Name = "abc company";
            fManager.SaveFirmInfo(firm);

        }
    }
}
