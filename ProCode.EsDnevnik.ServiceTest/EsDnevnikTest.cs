using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Security;
using System.Threading.Tasks;

namespace ProCode.EsDnevnik.ServiceTest
{
    [TestClass]
    public class EsDnevnikTest
    {
        [TestMethod]
        public void LoadLoginData()
        {
            var userCredential = Config.GetUserCredentials();

            string username = userCredential.GetUsername();
            Assert.IsNotNull(username, "Can''t read username.");
            Assert.AreNotEqual(string.Empty, username);

            string password = userCredential.GetPassword();
            Assert.IsNotNull(password, "Can''t read password.");
            Assert.AreNotEqual(0, password.Length);
        }

        [TestMethod]
        public void Login()
        {
            try
            {
                var userCredential = Config.GetUserCredentials();
                Service.EsDnevnik esd = new Service.EsDnevnik(userCredential);
                esd.LoginAsync().Wait();
            }
            catch (Exception ex)
            {
                Assert.ThrowsException<Exception>(() => throw ex);
            }
        }
    }
}
