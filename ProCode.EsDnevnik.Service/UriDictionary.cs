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

        public Uri GetLogoutUri()
        {
            return new Uri(baseUri, "/logout");
        }

        internal Uri GetBase()
        {
            return baseUri;
        }

        internal Uri GetStudentsUri()
        {
            return new Uri(baseUri, "/api/students");
        }

        internal Uri GetTimeLineEventsUri(Student student, int elementsToFetch = 30)
        {
            return new Uri(baseUri, $"/api/timeline/{student.Id}?take={elementsToFetch}&page=1&school={student.CurrentSchool.Id}&class={student.CurrentSchool.GetCurrentClass().RecordId}");
        }

        /// <summary>
        /// Get grades json.
        /// </summary>
        /// <param name="student"></param>
        /// <param name="elementsToFetch"></param>
        /// <returns></returns>
        internal Uri GetGradesUri(Student student, int elementsToFetch = 30)
        {
            return new Uri(baseUri, $"/api/grades/{student.GetLastStudentClassId()}");
        }
    }
}
