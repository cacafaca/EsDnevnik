using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProCode.EsDnevnikMob
{
    public class GradesComparer<T> : IComparer<T>

    {
        public int Compare(T x, T y)
        {
            // If some value is null. Three combinations.
            if (x == null && y != null)
                return -1;
            else if (x == null && y == null)
                return 0;
            else if (x != null && y == null)
                return 1;
            else
            {
                // If some value is not CourseGrade type.
                if (!(x is EsDnevnik.Model.GeneratedGrades.CourseGrades) && y is EsDnevnik.Model.GeneratedGrades.CourseGrades)
                    return -1;
                else if (!(x is EsDnevnik.Model.GeneratedGrades.CourseGrades) && !(y is EsDnevnik.Model.GeneratedGrades.CourseGrades))
                    return 0;
                else if (x is EsDnevnik.Model.GeneratedGrades.CourseGrades && !(y is EsDnevnik.Model.GeneratedGrades.CourseGrades))
                    return 1;
                else
                {
                    // This is a regular part of logic, where both values are of the same type, CourseGrades. Then it is easy to compare.
                    var gradeX = x as EsDnevnik.Model.GeneratedGrades.CourseGrades;
                    var gradeY = y as EsDnevnik.Model.GeneratedGrades.CourseGrades;

                    if (gradeX.AverageCalculated == 0 && gradeY.AverageCalculated > 0)
                        return 1;
                    else if (gradeX.AverageCalculated == 0 && gradeY.AverageCalculated == 0)
                        return 0;
                    else if (gradeX.AverageCalculated > 0 && gradeY.AverageCalculated == 0)
                        return -1;
                    else
                    {
                        var difference = gradeX.AverageCalculated - gradeY.AverageCalculated;
                        return difference != 0 ? (int)((difference) / Math.Abs(difference)) : 0;
                    }
                }
            }
        }
    }
}
