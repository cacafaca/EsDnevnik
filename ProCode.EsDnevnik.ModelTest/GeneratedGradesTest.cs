using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
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

            Model.GeneratedGrades.Class1[] classArray = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.GeneratedGrades.Class1[]>(gradesJson);
            Assert.IsNotNull(classArray);
            Assert.AreNotSame(0, classArray.Length);
        }
    }
}
