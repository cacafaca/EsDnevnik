using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection.Metadata;
using System.Security;

namespace ProCode.EsDnevnik.ServiceTest
{
    static class Config
    {
        #region Public
        public static Service.UserCredential GetLoginUserCredentials()
        {
            return GetUserCredentials(GetConfiguration(loginDetailsFileName));
        }

        public static Service.UserCredential GetResetPaswordEmail()
        {
            return GetUserCredentials(GetConfiguration(resetPasswordDetailsFileName));
        }

        public static Service.UserCredential GetEmailCredentials()
        {
            return GetUserCredentials(GetConfiguration(emailDetailsFileName));
        }
        #endregion

        #region Private
        private const string loginDetailsFileName = "LoginDetails.json";                    // This file name must exists in this test project.
        private const string resetPasswordDetailsFileName = "ResetPasswordDetails.json";    // This file name must exists in this test project.
        private const string emailDetailsFileName = "EmailDetails.json";                    // This file name must exists in this test project.

        private static IConfigurationRoot GetConfiguration(string fileName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(fileName, optional: true, reloadOnChange: true);
            return builder.Build();
        }
        /// <summary>
        /// Parse Json username/password pair.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private static Service.UserCredential GetUserCredentials(IConfigurationRoot config)
        {
            var username = config.GetSection("username").Value;
            var password_str = config.GetSection("password").Value;
            SecureString password = new SecureString();
            foreach (char passChar in password_str.ToCharArray())
            {
                password.AppendChar(passChar);
            }
            return new Service.UserCredential(username, password);
        }
        #endregion 
    }
}
