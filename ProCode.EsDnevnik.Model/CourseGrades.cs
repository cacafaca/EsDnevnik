using System;
using System.Collections.Generic;
using System.Text;

namespace ProCode.EsDnevnik.Model
{
    public class CourseGrades
    {
        public string Course { get; set; }
        public string Date { get; set; }
        public int Grade { get; set; }
        public string FullGrade { get; set; }
        public string GradeCategory { get; set; }
        public string Note { get; set; }
        public float Average { get; set; }
    }
}
