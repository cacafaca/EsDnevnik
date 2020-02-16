using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ProCode.EsDnevnik.ModelTest
{
    [TestClass]
    public class GeneratedAbsencesTest
    {
        [TestMethod]
        public void Load_Absences_Only_Justified()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ProCode.EsDnevnik.ModelTest.Resources.ExampleAbsences.json";

            string absencesJson = string.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                absencesJson = reader.ReadToEnd();
            }

            Model.GeneratedAbsences.AbsencesRoot absences = JsonConvert.DeserializeObject<Model.GeneratedAbsences.AbsencesRoot>(absencesJson);
            Assert.IsNotNull(absences);
            Assert.AreEqual(1244249, absences.FirstOrDefault().Value.ClassCourseId);
            Assert.AreEqual("Српски језик", absences.FirstOrDefault().Value.Name);
            Assert.AreEqual(10, absences.Count);
        }

        [TestMethod]
        public void Load_Absences_Justified_And_Unjustified()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ProCode.EsDnevnik.ModelTest.Resources.ExampleAbsenceNotConfirmed.json";

            string absencesJson = string.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                absencesJson = reader.ReadToEnd();
            }

            Model.GeneratedAbsences.AbsencesRoot absences = JsonConvert.DeserializeObject<Model.GeneratedAbsences.AbsencesRoot>(absencesJson);
            Assert.IsNotNull(absences);
            Assert.AreEqual(1092244, absences.FirstOrDefault().Value.ClassCourseId);
            Assert.AreEqual("Српски језик", absences.FirstOrDefault().Value.Name);
            Assert.AreEqual("2020-01-20", absences.FirstOrDefault().Value.AbsentStatuses.Unregulated.Absents[0].WorkdayDate);
        }
    }
}
