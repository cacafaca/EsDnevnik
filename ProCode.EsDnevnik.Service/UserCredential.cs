using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace ProCode.EsDnevnik.Service
{
    public class UserCredential
    {
        public UserCredential(string username, SecureString password)
        {
            this.username = username;
            this.password = password;
        }
        private string username;
        private SecureString password;
        public string GetUsername()
        {
            return username;
        }
        public SecureString GetPassword()
        {
            return password;
        }
    }
}
