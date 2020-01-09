﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProCode.EsDnevnik.Model.GeneratedGrades
{
    public class Rootobject
    {
        public CourseGrades[] Courses { get; set; }
    }

    public class CourseGrades
    {
        public string Course { get; set; }
        public int ClassCourseId { get; set; }
        public int ClassCourseGradeTypeId { get; set; }
        public int Sequence { get; set; }
        public Parts Parts { get; set; }
        [JsonIgnore]
        public float AverageTotal
        {
            get
            { 
                return Parts.Part2.Average > 0 ? (Parts.Part1.Average + Parts.Part2.Average) / 2 : Parts.Part1.Average; 
            }
        }
    }

    public class Parts
    {
        // Semester 1
        [JsonProperty("1")]
        public Part Part1 { get; set; }

        // Semester 1
        [JsonProperty("2")]
        public Part Part2 { get; set; }
    }

    // Polugođe
    public class Part
    {
        public Grade[] Grades { get; set; }
        public object Final { get; set; }
        public float Average { get; set; }
        [JsonIgnore]
        public string GradesString
        {
            get
            {
                return string.Join(", ", Grades.Where(g => g.GradeValue > 0).Select(g => g.GradeValue).ToArray());
            }
        }
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
}
