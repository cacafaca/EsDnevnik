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
        public void Load_Absences()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ProCode.EsDnevnik.ModelTest.Resources.ExampleAbsences.json";

            string absencesJson = string.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                absencesJson = reader.ReadToEnd();
            }

            Model.GeneratedAbsences.Rootobject absences = JsonConvert.DeserializeObject<Model.GeneratedAbsences.Rootobject>(absencesJson);
            Assert.IsNotNull(absences);
            Assert.AreEqual(1244249, absences.FirstOrDefault().Value.ClassCourseId);
            Assert.AreEqual("Српски језик", absences.FirstOrDefault().Value.Name);
            Assert.AreEqual(10, absences.Count);
        }
    }
}
