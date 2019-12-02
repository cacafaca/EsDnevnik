using System;
using System.Collections.Generic;
using System.Text;

namespace ProCode.EsDnevnik.BL
{
    class UriDictionary
    {
        Uri baseUri;
        public UriDictionary()
        {
            baseUri = new Uri("https://moj.esdnevnik.rs");
        }
        public Uri GetLoginUri()
        {
            
            return new Uri(baseUri, "/login");
        }
    }
}
