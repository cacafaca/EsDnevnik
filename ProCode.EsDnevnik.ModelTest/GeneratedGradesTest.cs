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
    public class GeneratedGradesTest
    {
        [TestMethod]
        public void LoadGeneratedGrades()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ProCode.EsDnevnik.ModelTest.Resources.ExampleGrades.json";

            string gradesJson = string.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                gradesJson = reader.ReadToEnd();
            }

            Model.GeneratedGrades.GradesArray[] classArray = JsonConvert.DeserializeObject<Model.GeneratedGrades.GradesArray[]>(gradesJson);
            Assert.IsNotNull(classArray);
            Assert.AreNotSame(0, classArray.Length);

            Assert.AreEqual("Народна традиција (изборни)", classArray.First().Course);
            Assert.AreEqual(5, classArray.First().Parts.Part1Value.Grades.First().GradeValue);
        }
    }
}
