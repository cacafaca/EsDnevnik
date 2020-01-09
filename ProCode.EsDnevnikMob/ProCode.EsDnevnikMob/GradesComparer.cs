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
            if (x == null && y != null)
                return -1;
            else if (x == null && y == null)
                return 0;
            else if (x != null && y == null)
                return 1;
            else
            {
                if (!(x is EsDnevnik.Model.GeneratedGrades.CourseGrades) && y is EsDnevnik.Model.GeneratedGrades.CourseGrades)
                    return -1;
                else if (!(x is EsDnevnik.Model.GeneratedGrades.CourseGrades) && !(y is EsDnevnik.Model.GeneratedGrades.CourseGrades))
                    return 0;
                else if (x is EsDnevnik.Model.GeneratedGrades.CourseGrades && !(y is EsDnevnik.Model.GeneratedGrades.CourseGrades))
                    return 1;
                else
                {
                    var cgx = x as EsDnevnik.Model.GeneratedGrades.CourseGrades;
                    var cgy = y as EsDnevnik.Model.GeneratedGrades.CourseGrades;

                    if (cgx.AverageTotal == 0 && cgy.AverageTotal > 0)
                        return 1;
                    else if (cgx.AverageTotal == 0 && cgy.AverageTotal == 0)
                        return 0;
                    else if (cgx.AverageTotal > 0 && cgy.AverageTotal == 0)
                        return -1;
                    else
                        return (int)((cgx.AverageTotal - cgy.AverageTotal) / Math.Abs(cgx.AverageTotal - cgy.AverageTotal));
                }
            }
        }
    }
}
