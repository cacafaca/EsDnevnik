using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;

namespace ProCode.EsDnevnik.ServiceTest
{
    static class Config
    {
        public static Service.UserCredential GetUserCredentials()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("LoginDetails.json", optional: true, reloadOnChange: true);
            IConfigurationRoot config = builder.Build();
            var username = config.GetSection("username").Value;
            var password_str = config.GetSection("password").Value;
            SecureString password = new SecureString();
            foreach(char passChar in password_str.ToCharArray())
            {
                password.AppendChar(passChar);
            }
            return new Service.UserCredential(username, password);
        }
    }
}
