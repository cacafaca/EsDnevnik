using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProCode.EsDnevnik.Service;
using System;

namespace ProCode.EsDnevnik.ServiceTest
{
    [TestClass()]
    public class EmailTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            var email = new Email(Config.GetEmailCredentials());
            email.Login();
            email.Logout();
        }

        [TestMethod()]
        public void GetResetPasswordLinkTest()
        {
            var esdnevnik = new Service.EsDnevnik(Config.GetResetPaswordEmail());
            esdnevnik.ResetPasswordRequestAsync().Wait();

            var email = new Email(Config.GetEmailCredentials());
            email.Login();

            Uri resetPasswordUri = null;
            for (int i = 0; i < 10; i++)
            {
                resetPasswordUri = email.GetResetPasswordLink();
                if (resetPasswordUri != null && resetPasswordUri.ToString().Length > 0)
                    break;
                else
                    System.Threading.Thread.Sleep(1000);
            }

            email.Logout();

            Assert.IsNotNull(resetPasswordUri);
            Assert.IsTrue(resetPasswordUri.ToString().StartsWith("https://moj.esdnevnik.rs/auth/reset-password?t"));
        }

    }
}