using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ProCode.EsDnevnik.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Jmbg { get; set; }
        public string Gender { get; set; }
        public IList<School> Schools { get; set; }
        public Student()
        {
            Schools = new List<School>();
        }
    }
}
