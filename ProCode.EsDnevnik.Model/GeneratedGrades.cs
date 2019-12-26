using System;
using System.Collections.Generic;
using System.Text;

namespace ProCode.EsDnevnik.Model.GeneratedGrades
{

    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public string course { get; set; }
        public int classCourseId { get; set; }
        public int classCourseGradeTypeId { get; set; }
        public int sequence { get; set; }
        public Parts parts { get; set; }
    }

    public class Parts
    {
        public _1 _1 { get; set; }
        public _2 _2 { get; set; }
    }

    public class _1
    {
        public Grade[] grades { get; set; }
        public object final { get; set; }
        public float average { get; set; }
    }

    public class Grade
    {
        public bool descriptive { get; set; }
        public string date { get; set; }
        public string createTime { get; set; }
        public string fullGrade { get; set; }
        public int grade { get; set; }
        public string gradeCategory { get; set; }
        public string note { get; set; }
        public object schoolyearPartId { get; set; }
        public object evaluationElement { get; set; }
    }

    public class _2
    {
        public object[] grades { get; set; }
        public object final { get; set; }
        public int average { get; set; }
    }
}
