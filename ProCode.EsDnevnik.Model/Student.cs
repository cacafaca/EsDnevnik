﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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
        public School CurrentSchool
        {
            get
            {
                //return "test";
                return Schools.FirstOrDefault();
            }
        }

        public SchoolYear GetLastSchoolYear()
        {
            return Schools.FirstOrDefault()?.SchoolYears.OrderByDescending(sy => sy.Year).FirstOrDefault();
        }

        public int? GetLastStudentClassId()
        {
            return GetLastSchoolYear().Classes.OrderByDescending(classId => classId.RecordId).FirstOrDefault()?.StudentClassId;
        }
    }
}
