using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

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

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
            }
        }
    }
}
