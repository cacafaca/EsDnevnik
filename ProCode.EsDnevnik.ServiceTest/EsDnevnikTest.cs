using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProCode.EsDnevnik.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace ProCode.EsDnevnik.ServiceTest
{
    [TestClass]
    public class EsDnevnikTest
    {
        [TestMethod]
        public void Load_Login_Data()
        {
            var userCredential = Config.GetLoginUserCredentials();

            string username = userCredential.GetUsername();
            Assert.IsNotNull(username, "Can''t read username.");
            Assert.AreNotEqual(string.Empty, username);

            string password = userCredential.GetPassword();
            Assert.IsNotNull(password, "Can''t read password.");
            Assert.AreNotEqual(0, password.Length);
        }

        [TestMethod]
        public void Login_And_Logout()
        {
            var userCredential = Config.GetLoginUserCredentials();
            Service.EsDnevnik esd = new Service.EsDnevnik(userCredential);
            esd.LoginAsync().Wait();
            esd.LogoutAsync().Wait();
        }

        [TestMethod]
        public void Login_Get_Students_Logout()
        {
            var userCredential = Config.GetLoginUserCredentials();
            Service.EsDnevnik esd = new Service.EsDnevnik(userCredential);

            try
            {
                esd.LoginAsync().Wait();
                IList<Student> students = esd.GetStudentsAsync().Result;
                Assert.IsNotNull(students);
                Assert.AreNotEqual(0, students.Count);
            }
            finally
            {
                esd.LogoutAsync().Wait();
            }
        }

        [TestMethod]
        public void Login_GetTimeLineEvents_Logout()
        {
            var userCredential = Config.GetLoginUserCredentials();
            Service.EsDnevnik esd = new Service.EsDnevnik(userCredential);

            try
            {
                esd.LoginAsync().Wait();

                // Get students.
                IList<Student> students = esd.GetStudentsAsync().Result;
                Assert.IsNotNull(students);
                Assert.AreNotEqual(0, students.Count, "No students.");

                // Get events.
                Model.GeneratedTimeLine.Rootobject rootTimeLine = esd.GetTimeLineEventsAsync(students.First()).Result;
                Assert.IsNotNull(rootTimeLine);
                Assert.IsNotNull(rootTimeLine.Data);
                Assert.AreNotEqual(0, rootTimeLine.Data.Count);
            }
            finally
            {
                esd.LogoutAsync().Wait();
            }
        }

        [TestMethod]
        public void GetGrades()
        {
            var userCredential = Config.GetLoginUserCredentials();
            Service.EsDnevnik esd = new Service.EsDnevnik(userCredential);

            try
            {
                esd.LoginAsync().Wait();

                // Get students.
                IList<Student> students = esd.GetStudentsAsync().Result;
                Assert.IsNotNull(students);
                Assert.AreNotEqual(0, students.Count, "No students.");

                // Get events.
                Model.GeneratedGrades.Rootobject gradesRoot = esd.GetGradesAsync(students.First()).Result;
                Assert.IsNotNull(gradesRoot);
                Assert.IsNotNull(gradesRoot.Courses);
                Assert.AreNotEqual(0, gradesRoot.Courses.Length, "No grades.");
            }
            finally
            {
                esd.LogoutAsync().Wait();
            }
        }

        [TestMethod]
        public void GetAbsences()
        {
            var userCredential = Config.GetLoginUserCredentials();
            Service.EsDnevnik esd = new Service.EsDnevnik(userCredential);

            try
            {
                esd.LoginAsync().Wait();

                // Get students.
                IList<Student> students = esd.GetStudentsAsync().Result;
                Assert.IsNotNull(students);
                Assert.AreNotEqual(0, students.Count, "No students.");

                // Get events.
                Model.GeneratedAbsences.AbsencesRoot absences = esd.GetAbsencesAsync(students.First()).Result;
                Assert.IsNotNull(absences);
                Assert.AreNotEqual(0, absences.Count);
            }
            finally
            {
                esd.LogoutAsync().Wait();
            }
        }

        [TestMethod]
        public void Send_Reset_Password_Request()
        {
            var resetPasswordCredential = Config.GetResetPaswordEmail();
            Service.EsDnevnik esd = new Service.EsDnevnik(resetPasswordCredential);

            try
            {
                esd.ResetPasswordRequestAsync().Wait();
            }
            finally
            {
                esd.LogoutAsync().Wait();
            }

        }
    }
}
