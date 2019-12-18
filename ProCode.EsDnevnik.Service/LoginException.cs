using System;

namespace ProCode.EsDnevnik.Service
{
    public class LoginException : Exception
    {
        public LoginException(System.Net.HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
        public System.Net.HttpStatusCode StatusCode { get; private set; }
    }
}
