using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProCode.EsDnevnik.BLTest
{
    [TestClass]
    public class EsDnevnikTest
    {
        [TestMethod]
        public void Login()
        {
            try
            {
                BL.EsDnevnik esd = new BL.EsDnevnik(null);
                esd.LoginAsync();
            }
            catch (Exception ex)
            {
                throw new AssertFailedException("Nije uspeo.", ex);
            }
        }
    }
}
