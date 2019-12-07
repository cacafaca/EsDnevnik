using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Security;

namespace ProCode.EsDnevnik.ServiceTest
{
    [TestClass]
    public class EsDnevnikTest
    {
        [TestMethod]
        public void LoadLoginData()
        {
            string testVAlue = ConfigurationManager.AppSettings["testkey"];
            Assert.IsNotNull(testVAlue);

            string username = ConfigurationManager.AppSettings.Get("username");
            Assert.IsNotNull(username, "Can''t read username.");
            string password = ConfigurationManager.AppSettings.Get("password");
            Assert.IsNotNull(password, "Can''t read password.");
        }

        [TestMethod]
        public void Login()
        {
            try
            {
                string username = ConfigurationManager.AppSettings.Get("username");
                string password = ConfigurationManager.AppSettings.Get("password");
                Service.EsDnevnik esd = new Service.EsDnevnik(null);
                esd.LoginAsync();
            }
            catch (Exception ex)
            {
                throw new AssertFailedException("Nije uspeo.", ex);
            }
        }
    }
}
