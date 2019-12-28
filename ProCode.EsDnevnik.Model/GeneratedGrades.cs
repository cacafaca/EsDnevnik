﻿using Newtonsoft.Json;
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
        public string Course { get; set; }
        public int ClassCourseId { get; set; }
        public int ClassCourseGradeTypeId { get; set; }
        public int Sequence { get; set; }
        public Parts Parts { get; set; }
    }

    public class Parts
    {
        [JsonProperty("1")]
        public Part1 Part1Value { get; set; }
        [JsonProperty("2")]
        public Part2 Part2Value { get; set; }
    }

    public class Part1
    {
        public Grade[] Grades { get; set; }
        public object Final { get; set; }
        public float Average { get; set; }
    }

    public class Grade
    {
        public bool Descriptive { get; set; }
        public string Date { get; set; }
        public string CreateTime { get; set; }
        public string FullGrade { get; set; }
        [JsonProperty("grade")]
        public int GradeValue { get; set; }
        public string GradeCategory { get; set; }
        public string Note { get; set; }
        public object SchoolyearPartId { get; set; }
        public object EvaluationElement { get; set; }
    }

    public class Part2
    {
        public object[] Grades { get; set; }
        public object Final { get; set; }
        public int Average { get; set; }
    }
}
