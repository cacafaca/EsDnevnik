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
        public void Load_Grades_First_Semester_Not_Finished()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ProCode.EsDnevnik.ModelTest.Resources.ExampleGradesSemester1NotFinished.json";

            string gradesJson = string.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                gradesJson = reader.ReadToEnd();
            }

            Model.GeneratedGrades.CourseGrades[] classArray = JsonConvert.DeserializeObject<Model.GeneratedGrades.CourseGrades[]>(gradesJson);
            Assert.IsNotNull(classArray);
            Assert.AreNotSame(0, classArray.Length);

            Assert.AreEqual("Народна традиција (изборни)", classArray.First().Course);
            Assert.AreEqual(5, classArray.First().Parts.Part1.Grades.First().GradeValue);
        }

        [TestMethod]
        public void Load_Grades_Second_Semester_Finished()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ProCode.EsDnevnik.ModelTest.Resources.ExampleGradesSemester2Finished.json";

            string gradesJson = string.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                gradesJson = reader.ReadToEnd();
            }

            Model.GeneratedGrades.CourseGrades[] classArray = JsonConvert.DeserializeObject<Model.GeneratedGrades.CourseGrades[]>(gradesJson);
            Assert.IsNotNull(classArray);
            Assert.AreNotSame(0, classArray.Length);

            Assert.AreEqual("Народна традиција (изборни)", classArray.First().Course);
            Assert.AreEqual(5, classArray.First().Parts.Part1.Grades.First().GradeValue);
            Assert.AreEqual(5, classArray.First().Parts.Part1.Final.Value);
            Assert.AreEqual(5, classArray.First().Parts.Part2.Final.Value);
        }
    }
}
