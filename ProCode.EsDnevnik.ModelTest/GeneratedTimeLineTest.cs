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
    public class GeneratedTimeLineTest
    {
        /// <summary>
        /// De-serialize time line into generated structure.
        /// </summary>
        [TestMethod]
        public void LoadGeneratedTimeLine()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ProCode.EsDnevnik.ModelTest.Resources.ExampleTimeLine.json";

            string timeLineJson = string.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                timeLineJson = reader.ReadToEnd();
            }

            Model.GeneratedTimeLine.Rootobject root = JsonConvert.DeserializeObject<Model.GeneratedTimeLine.Rootobject>(timeLineJson);
            Assert.IsNotNull(root);

            System.Console.WriteLine(root.Data.First().Value[0].Summary);

            Assert.IsNotNull(root.Data);
            Assert.AreEqual(14, root.Data.Count);

            Assert.AreEqual(1, root.Data.ToArray()[0].Value.Length);
            Assert.AreEqual(2, root.Data.ToArray()[1].Value.Length);

            Assert.IsNotNull(root.Meta);

            //Assert.AreNotEqual(0, root.Data.EventDate.Count);
            //Assert.AreEqual(5, data.First().Parts.Part1Value.Grades.First().GradeValue);
        }
    }
}
