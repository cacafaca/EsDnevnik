using System;
using System.Net.Http;

namespace ProCode.EsDnevnik.BL
{
    public class EsDnevnik
    {
        #region Private Properties
        UserCredential userCredential;
        HttpClient client;
        UriDictionary uriDictionary;
        #endregion 

        #region Constructors
        public EsDnevnik(UserCredential userCredential)
        {
            this.userCredential = userCredential;
            client = new HttpClient();
            uriDictionary = new UriDictionary();
        }
        #endregion

        #region Public methods
        public async void LoginAsync()
        {
            string token = string.Empty;
            HttpContent content = new StringContent($"_token={token}&username={userCredential.GetUsername()}&password={userCredential.GetPassword()}");
            HttpResponseMessage msg = await client.PostAsync(uriDictionary.GetLoginUri(), content);

        }
        #endregion
    }
}
