using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace ProCode.EsDnevnik.Service
{
    public class UserCredential
    {
        public UserCredential(string username, SecureString password)
        {
            this.username = username.Trim();
            this.password = password;
        }
        private readonly string username;
        private readonly SecureString password;
        public string GetUsername()
        {
            return username;
        }
        public string GetPassword()
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(password);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
