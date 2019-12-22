using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ProCode.EsDnevnikMob
{
    class UserSettings
    {
        const string usernameKey = "username";
        const string passwordKey = "password";

        public async Task<string> GetUsernameAsync()
        {
            return await SecureStorage.GetAsync(usernameKey);
        }
        public async Task<string> GetPasswordAsync()
        {
            return await SecureStorage.GetAsync(passwordKey);
        }
        public async Task SetUsernameAsync(string username)
        {
            await SecureStorage.SetAsync(usernameKey, username);
        }
        public async Task SetPasswordAsync(string password)
        {
            await SecureStorage.SetAsync(passwordKey, password);
        }
    }
}
