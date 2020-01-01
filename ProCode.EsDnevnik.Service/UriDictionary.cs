using ProCode.EsDnevnik.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProCode.EsDnevnik.Service
{
    class UriDictionary
    {
        readonly Uri baseUri;
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

        internal Uri GetTimeLineEventsUri(Student student, ref int page, int elementsToFetch = 30)
        {
            if (page < 1)
                page = 1;
            return new Uri(baseUri, $"/api/timeline/{student.Id}?take={elementsToFetch}&page={page}&school={student.CurrentSchool.Id}&class={student.CurrentSchool.GetCurrentClass().RecordId}");
        }

        /// <summary>
        /// Get grades json.
        /// </summary>
        /// <param name="student"></param>
        /// 
        /// <returns></returns>
        internal Uri GetGradesUri(Student student)
        {
            return new Uri(baseUri, $"/api/grades/{student.GetLastStudentClassId()}");
        }
    }
}
