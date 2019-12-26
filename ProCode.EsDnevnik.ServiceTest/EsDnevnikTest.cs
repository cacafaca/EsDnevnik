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
        public void LoadLoginData()
        {
            var userCredential = Config.GetUserCredentials();

            string username = userCredential.GetUsername();
            Assert.IsNotNull(username, "Can''t read username.");
            Assert.AreNotEqual(string.Empty, username);

            string password = userCredential.GetPassword();
            Assert.IsNotNull(password, "Can''t read password.");
            Assert.AreNotEqual(0, password.Length);
        }

        [TestMethod]
        public void Login()
        {
            var userCredential = Config.GetUserCredentials();
            Service.EsDnevnik esd = new Service.EsDnevnik(userCredential);

            try
            {
                esd.LoginAsync().Wait();
            }
            finally
            {
                esd.LogoutAsync().Wait();
            }
        }

        [TestMethod]
        public void GetStudents()
        {
            var userCredential = Config.GetUserCredentials();
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
        public void GetTimeLineEvents()
        {
            var userCredential = Config.GetUserCredentials();
            Service.EsDnevnik esd = new Service.EsDnevnik(userCredential);

            try
            {
                esd.LoginAsync().Wait();

                // Get students.
                IList<Student> students = esd.GetStudentsAsync().Result;
                Assert.IsNotNull(students);
                Assert.AreNotEqual(0, students.Count, "No students.");

                // Get events.
                IList<TimeLineEvent> timeLineEvents = esd.GetTimeLineEventsAsync(students.First()).Result;
                Assert.IsNotNull(timeLineEvents);
                Assert.AreNotEqual(0, timeLineEvents.Count, "No events.");
            }
            finally
            {
                esd.LogoutAsync().Wait();
            }
        }

        [TestMethod]
        public void GetGrades()
        {
            var userCredential = Config.GetUserCredentials();
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
                Assert.IsNotNull(gradesRoot.Property1);
                Assert.AreNotEqual(0, gradesRoot.Property1.Length, "No grades.");
            }
            finally
            {
                esd.LogoutAsync().Wait();
            }
        }
    }
}
