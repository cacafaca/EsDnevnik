using ProCode.EsDnevnik.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProCode.EsDnevnik.Service
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

        internal Uri GetBase()
        {
            return baseUri;
        }

        internal Uri GetStudentsUri()
        {
            return new Uri(baseUri, "/api/students");
        }

        internal Uri GetTimeLineEventsUri(Student student)
        {
            return new Uri(baseUri, $"/api/timeline/{student.Id}?take=100&page=1&school={student.CurrentSchool.Id}&class={student.CurrentSchool.GetCurrentClass().RecordId}");
        }
    }
}
