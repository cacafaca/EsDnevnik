using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ProCode.EsDnevnik.Service
{
    public static class FakeData
    {
        public static string GetFakeResource(string resourceName)
        {
            string absencesJson = null;
            var assembly = Assembly.GetExecutingAssembly();
            if (assembly != null)
            {
                var resourceFullName = $"{assembly.GetName().Name}.Resources.{resourceName}";

                using (Stream stream = assembly.GetManifestResourceStream(resourceFullName))
                {
                    if (stream != null)
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            absencesJson = reader.ReadToEnd();
                        }
                }
            }
            return absencesJson;
        }
        public static string GetFakeTimeLineEvents()
        {
#if DEBUGFAKE
            return GetFakeResource("FakeTimeLineEvents.json");
#else
            return string.Empty;
#endif
        }

        public static string GetFakeGradesJson()
        {
#if DEBUGFAKE
            return @"[
  {
    ""course"": ""Народна традиција(изборни)"",
    ""classCourseId"": 266783,
    ""classCourseGradeTypeId"": 1,
    ""sequence"": 10,
    ""parts"": {
                ""1"": {
                    ""grades"": [
                      {
            ""descriptive"": false,
                        ""date"": ""2018-10-08"",
                        ""createTime"": ""2018-10-08 12:32:18"",
                        ""fullGrade"": ""одличан (5)"",
                        ""grade"": 5,
                        ""gradeCategory"": ""практичан рад"",
                        ""note"": ""Припрема данашњег часа (пројекат ученика)."",
                        ""schoolyearPartId"": null,
                        ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-12-24"",
            ""createTime"": ""2018-12-24 22:31:54"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": ""Пројекат ученика"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-14"",
            ""createTime"": ""2019-01-29 17:26:17"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": ""Mакета кућице из прошлости."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 1,
          ""engagement"": null
        },
        ""average"": 5
      },
      ""2"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2019-04-08"",
            ""createTime"": ""2019-04-08 19:14:30"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-04-22"",
            ""createTime"": ""2019-04-22 15:18:31"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": ""Пројекат ученика."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-05-20"",
            ""createTime"": ""2019-05-20 14:16:14"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": ""Пројекат ученика \""Варошице\""."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-06-11"",
            ""createTime"": ""2019-06-11 13:49:12"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""вежбе"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 2,
          ""engagement"": null
        },
        ""average"": 5
      }
    }
  },
  {
    ""course"": ""ЧОС"",
    ""classCourseId"": 266951,
    ""classCourseGradeTypeId"": 2,
    ""sequence"": 11,
    ""parts"": {
      ""1"": {
        ""grades"": [
          {
            ""descriptive"": true,
            ""date"": ""2018-11-07"",
            ""createTime"": ""2018-11-11 21:59:02"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": true,
            ""date"": ""2019-01-16"",
            ""createTime"": ""2019-01-30 14:13:32"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""истиче се"",
          ""value"": 0,
          ""schoolyear_part_id"": 1,
          ""engagement"": null
        },
        ""average"": 0
      },
      ""2"": {
        ""grades"": [
          {
            ""descriptive"": true,
            ""date"": ""2019-04-01"",
            ""createTime"": ""2019-04-07 16:13:55"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": true,
            ""date"": ""2019-05-22"",
            ""createTime"": ""2019-06-13 20:27:05"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": true,
            ""date"": ""2019-06-12"",
            ""createTime"": ""2019-06-13 08:11:49"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""истиче се"",
          ""value"": 0,
          ""schoolyear_part_id"": 2,
          ""engagement"": null
        },
        ""average"": 0
      }
    }
  },
  {
    ""course"": ""Математичка секција (слободна наставна активност)"",
    ""classCourseId"": 267196,
    ""classCourseGradeTypeId"": 2,
    ""sequence"": 14,
    ""parts"": {
      ""1"": {
        ""grades"": [
          {
            ""descriptive"": true,
            ""date"": ""2018-10-26"",
            ""createTime"": ""2018-10-28 21:50:26"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": true,
            ""date"": ""2019-01-18"",
            ""createTime"": ""2019-01-18 17:11:57"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": ""Рад на задацима."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""истиче се"",
          ""value"": 0,
          ""schoolyear_part_id"": 1,
          ""engagement"": null
        },
        ""average"": 0
      },
      ""2"": {
        ""grades"": [
          {
            ""descriptive"": true,
            ""date"": ""2019-04-05"",
            ""createTime"": ""2019-04-07 16:10:21"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": true,
            ""date"": ""2019-05-17"",
            ""createTime"": ""2019-06-13 20:49:05"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": true,
            ""date"": ""2019-06-07"",
            ""createTime"": ""2019-06-07 16:32:02"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""истиче се"",
          ""value"": 0,
          ""schoolyear_part_id"": 2,
          ""engagement"": null
        },
        ""average"": 0
      }
    }
  },
  {
    ""course"": ""Српски језик"",
    ""classCourseId"": 266651,
    ""classCourseGradeTypeId"": 1,
    ""sequence"": 1,
    ""parts"": {
      ""1"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2018-10-09"",
            ""createTime"": ""2018-10-09 12:22:05"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""остало"",
            ""note"": ""Мини провера (5)"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-10-15"",
            ""createTime"": ""2018-10-15 17:53:26"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""вежбе"",
            ""note"": ""Правописна вежба"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-10-30"",
            ""createTime"": ""2018-10-30 19:40:15"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""45/50 поена"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-11-14"",
            ""createTime"": ""2018-11-13 23:14:46"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""57/57 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-11-26"",
            ""createTime"": ""2018-11-23 19:31:20"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""100/100 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-12-18"",
            ""createTime"": ""2018-12-17 23:17:10"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""70/73 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-09"",
            ""createTime"": ""2019-01-10 21:25:17"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""вежбе"",
            ""note"": ""Писмени састав \""Доживљај са зимског распуста\"""",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-23"",
            ""createTime"": ""2019-01-22 21:13:18"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""66/66 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-30"",
            ""createTime"": ""2019-01-30 15:33:24"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": ""Укупно ангажовање ученика у току првог полугодишта - активност на часу."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 1,
          ""engagement"": null
        },
        ""average"": 5
      },
      ""2"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2019-03-08"",
            ""createTime"": ""2019-03-08 23:11:54"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""56,5,/60 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-03-26"",
            ""createTime"": ""2019-03-26 19:29:57"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""67/70 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-04-18"",
            ""createTime"": ""2019-04-18 17:16:09"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""51,5/53 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-04-24"",
            ""createTime"": ""2019-04-24 18:58:36"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""вежбе"",
            ""note"": ""Правописна вежба - диктат"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-05-08"",
            ""createTime"": ""2019-05-08 17:59:36"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""вежбе"",
            ""note"": ""Диктат писаним словима латинице"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-05-10"",
            ""createTime"": ""2019-05-10 13:29:12"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": ""Обрада лектире."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-05-17"",
            ""createTime"": ""2019-05-17 21:44:41"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-05-24"",
            ""createTime"": ""2019-05-24 17:25:11"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""79,5/87 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-06-10"",
            ""createTime"": ""2019-06-11 11:31:17"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-06-12"",
            ""createTime"": ""2019-06-12 17:31:04"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 2,
          ""engagement"": null
        },
        ""average"": 5
      }
    }
  },
  {
    ""course"": ""Енглески језик (1. страни језик)"",
    ""classCourseId"": 266662,
    ""classCourseGradeTypeId"": 1,
    ""sequence"": 2,
    ""parts"": {
      ""1"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2018-10-10"",
            ""createTime"": ""2018-10-10 09:46:52"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""усмено одговарање"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-10-10"",
            ""createTime"": ""2018-10-10 09:48:07"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-11-14"",
            ""createTime"": ""2018-11-14 15:26:03"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""усмено одговарање"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-11-19"",
            ""createTime"": ""2018-11-19 09:02:18"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""усмено одговарање"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-12-24"",
            ""createTime"": ""2018-12-24 14:33:18"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""усмено одговарање"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-23"",
            ""createTime"": ""2019-01-23 15:24:00"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""усмено одговарање"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-28"",
            ""createTime"": ""2019-01-28 08:45:34"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""усмено одговарање"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 1,
          ""engagement"": null
        },
        ""average"": 5
      },
      ""2"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2019-03-13"",
            ""createTime"": ""2019-03-13 09:23:44"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-04-01"",
            ""createTime"": ""2019-04-01 14:13:51"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""усмено одговарање"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-05-13"",
            ""createTime"": ""2019-05-13 14:30:24"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""усмено одговарање"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-06-05"",
            ""createTime"": ""2019-06-05 09:43:22"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""усмено одговарање"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-06-10"",
            ""createTime"": ""2019-06-10 14:10:22"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""усмено одговарање"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 2,
          ""engagement"": null
        },
        ""average"": 5
      }
    }
  },
  {
    ""course"": ""Математика"",
    ""classCourseId"": 266635,
    ""classCourseGradeTypeId"": 1,
    ""sequence"": 3,
    ""parts"": {
      ""1"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2018-09-19"",
            ""createTime"": ""2018-09-20 18:35:25"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-10-02"",
            ""createTime"": ""2018-10-02 20:25:25"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""остало"",
            ""note"": ""2.10.2018. Мини провера (5)"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-10-12"",
            ""createTime"": ""2018-10-12 14:11:47"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""36/36 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-10-30"",
            ""createTime"": ""2018-10-30 19:06:57"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""вежбе"",
            ""note"": ""Рад на задацима на часовима утврђивања (мини провере - средња просечна оцена)."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-11-01"",
            ""createTime"": ""2018-11-01 21:25:32"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-11-19"",
            ""createTime"": ""2018-11-17 09:15:47"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""38/38 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-12-07"",
            ""createTime"": ""2018-12-06 19:29:39"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""39/39 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-12-17"",
            ""createTime"": ""2018-12-14 19:45:14"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""26/26 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-16"",
            ""createTime"": ""2019-01-16 23:29:33"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": ""Решени сви задаци о множењу на сва три нивоа тежине."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-22"",
            ""createTime"": ""2019-01-21 23:39:41"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""50/50 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-30"",
            ""createTime"": ""2019-01-30 15:05:10"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": ""Досадашња знања о множењу (мини провере и активност на часу)."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 1,
          ""engagement"": null
        },
        ""average"": 5
      },
      ""2"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2019-03-05"",
            ""createTime"": ""2019-03-05 22:35:55"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""34+/34 бодова (Ученица је решила це тест и додатни задатак тачно)."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-03-16"",
            ""createTime"": ""2019-03-15 19:42:42"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""40/40 бодова (решен и додатни задатак)"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-04-09"",
            ""createTime"": ""2019-04-09 11:24:37"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-04-16"",
            ""createTime"": ""2019-04-15 19:11:48"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""43/44 бодова (решила и додатни задатак)"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-05-10"",
            ""createTime"": ""2019-05-10 13:42:05"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""вежбе"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-05-15"",
            ""createTime"": ""2019-05-17 21:41:20"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""46/46 бодова (решила и додатни задатак)"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-05-24"",
            ""createTime"": ""2019-05-24 17:30:46"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-06-13"",
            ""createTime"": ""2019-06-13 07:52:58"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""77,75/80 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-06-13"",
            ""createTime"": ""2019-06-13 14:31:25"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 2,
          ""engagement"": null
        },
        ""average"": 5
      }
    }
  },
  {
    ""course"": ""Свет око нас"",
    ""classCourseId"": 266669,
    ""classCourseGradeTypeId"": 1,
    ""sequence"": 4,
    ""parts"": {
      ""1"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2018-10-15"",
            ""createTime"": ""2018-10-12 14:34:46"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""65,5/70 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-10-22"",
            ""createTime"": ""2018-10-22 12:23:30"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""усмено одговарање"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-11-07"",
            ""createTime"": ""2018-11-07 13:59:09"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-11-19"",
            ""createTime"": ""2018-11-19 19:05:09"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-11-28"",
            ""createTime"": ""2018-11-26 23:36:02"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""66,5/74 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-12-19"",
            ""createTime"": ""2018-12-21 22:14:27"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""остало"",
            ""note"": ""Оцена из две мини провере из наставне области \""Насеље\"""",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-09"",
            ""createTime"": ""2018-12-27 01:14:22"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""60,5/64 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-30"",
            ""createTime"": ""2019-01-30 14:06:11"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""54,5/60 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 1,
          ""engagement"": null
        },
        ""average"": 5
      },
      ""2"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2019-03-23"",
            ""createTime"": ""2019-03-23 12:59:06"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""усмено одговарање"",
            ""note"": ""1. Наброј пет начина обраде материјала. \n2. Који се материјали могу лепити?\n3. Шта су изолатори топлоте?\n4. Који су то материјали?"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-04-01"",
            ""createTime"": ""2019-03-28 21:24:48"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""59,5/60 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-04-24"",
            ""createTime"": ""2019-04-23 20:21:23"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""39/40 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-05-22"",
            ""createTime"": ""2019-05-22 15:17:08"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""усмено одговарање"",
            ""note"": ""Кретање у простору. Дан."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-06-12"",
            ""createTime"": ""2019-06-10 21:25:26"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""контролни задатак"",
            ""note"": ""68/70 бодова"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 2,
          ""engagement"": null
        },
        ""average"": 5
      }
    }
  },
  {
    ""course"": ""Ликовна култура"",
    ""classCourseId"": 266695,
    ""classCourseGradeTypeId"": 1,
    ""sequence"": 5,
    ""parts"": {
      ""1"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2018-10-11"",
            ""createTime"": ""2018-10-11 22:17:12"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-11-01"",
            ""createTime"": ""2018-11-01 21:20:11"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-12-13"",
            ""createTime"": ""2018-12-13 21:16:20"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-17"",
            ""createTime"": ""2019-01-17 17:39:51"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 1,
          ""engagement"": null
        },
        ""average"": 5
      },
      ""2"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2019-03-28"",
            ""createTime"": ""2019-03-28 21:13:14"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-04-11"",
            ""createTime"": ""2019-04-12 15:12:33"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": ""\""Рељеф\"", ускршњи мотив"",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-05-16"",
            ""createTime"": ""2019-05-17 00:16:52"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-06-06"",
            ""createTime"": ""2019-06-06 21:03:09"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 2,
          ""engagement"": null
        },
        ""average"": 5
      }
    }
  },
  {
    ""course"": ""Музичка култура"",
    ""classCourseId"": 266677,
    ""classCourseGradeTypeId"": 1,
    ""sequence"": 6,
    ""parts"": {
      ""1"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2018-10-16"",
            ""createTime"": ""2018-10-16 17:21:46"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-11-13"",
            ""createTime"": ""2018-11-14 10:44:23"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-15"",
            ""createTime"": ""2019-01-16 23:45:36"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""вежбе"",
            ""note"": ""Слушање и стварање музике."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-29"",
            ""createTime"": ""2019-01-29 15:34:49"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""вежбе"",
            ""note"": ""Музичка писменост и дечје стваралаштво."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 1,
          ""engagement"": null
        },
        ""average"": 5
      },
      ""2"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2019-04-02"",
            ""createTime"": ""2019-04-08 23:48:23"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-05-21"",
            ""createTime"": ""2019-06-10 09:20:48"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""вежбе"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-06-04"",
            ""createTime"": ""2019-06-10 09:28:34"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-06-11"",
            ""createTime"": ""2019-06-13 19:59:26"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""вежбе"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 2,
          ""engagement"": null
        },
        ""average"": 5
      }
    }
  },
  {
    ""course"": ""Физичко васпитање"",
    ""classCourseId"": 266685,
    ""classCourseGradeTypeId"": 1,
    ""sequence"": 7,
    ""parts"": {
      ""1"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2018-10-26"",
            ""createTime"": ""2018-10-31 08:15:10"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2018-11-08"",
            ""createTime"": ""2018-11-11 22:01:57"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-10"",
            ""createTime"": ""2019-01-10 21:11:48"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-01-22"",
            ""createTime"": ""2019-01-29 15:25:36"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 1,
          ""engagement"": null
        },
        ""average"": 5
      },
      ""2"": {
        ""grades"": [
          {
            ""descriptive"": false,
            ""date"": ""2019-04-05"",
            ""createTime"": ""2019-04-05 22:49:58"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-04-09"",
            ""createTime"": ""2019-04-09 11:30:46"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""активност на часу"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-05-24"",
            ""createTime"": ""2019-05-24 16:32:18"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""вежбе"",
            ""note"": ""Прескакање дуге вијаче."",
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": false,
            ""date"": ""2019-06-07"",
            ""createTime"": ""2019-06-07 16:24:55"",
            ""fullGrade"": ""одличан (5)"",
            ""grade"": 5,
            ""gradeCategory"": ""практичан рад"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""одличан"",
          ""value"": 5,
          ""schoolyear_part_id"": 2,
          ""engagement"": null
        },
        ""average"": 5
      }
    }
  },
  {
    ""course"": ""Верска настава - православни катихизис (изборни)"",
    ""classCourseId"": 266757,
    ""classCourseGradeTypeId"": 2,
    ""sequence"": 9,
    ""parts"": {
      ""1"": {
        ""grades"": [
          {
            ""descriptive"": true,
            ""date"": ""2018-11-02"",
            ""createTime"": ""2018-11-03 13:17:21"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": true,
            ""date"": ""2019-01-25"",
            ""createTime"": ""2019-01-25 20:46:14"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""истиче се"",
          ""value"": 0,
          ""schoolyear_part_id"": 1,
          ""engagement"": null
        },
        ""average"": 0
      },
      ""2"": {
        ""grades"": [
          {
            ""descriptive"": true,
            ""date"": ""2019-04-05"",
            ""createTime"": ""2019-04-08 20:18:20"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          },
          {
            ""descriptive"": true,
            ""date"": ""2019-06-10"",
            ""createTime"": ""2019-06-10 20:54:19"",
            ""fullGrade"": ""истиче се"",
            ""grade"": 0,
            ""gradeCategory"": ""описна"",
            ""note"": null,
            ""schoolyearPartId"": null,
            ""evaluationElement"": null
          }
        ],
        ""final"": {
          ""name"": ""истиче се"",
          ""value"": 0,
          ""schoolyear_part_id"": 2,
          ""engagement"": null
        },
        ""average"": 0
      }
    }
  }
]
";
#else
            return string.Empty;
#endif
        }

        public static string GetFakeAbsencesJson()
        {
#if DEBUGFAKE
            return @"
{
  ""1_1_0"": {
    ""classCourseId"": 1092244,
    ""name"": ""Српски језик"",
    ""sequence"": ""1_1_0"",
    ""absentStatuses"": {
                ""1"": {
                    ""statusId"": 1,
        ""name"": ""нерегулисан"",
        ""absents"": [
          {
            ""id"": 67628119,
            ""workHourId"": 48444467,
            ""teacherNote"": null,
            ""statusName"": ""нерегулисан"",
            ""workHourNote"": ""92. Припрема за контролни (врсте и служба речи)"",
            ""workdayDate"": ""2020-01-20""
          }
        ]
      },
      ""2"": {
        ""statusId"": 3,
        ""name"": ""оправдан"",
        ""absents"": [
          {
            ""id"": 60439176,
            ""workHourId"": 43301797,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""72. Читање и анализа домаћег задатка: Необично писмо"",
            ""workdayDate"": ""2019-12-11""
          },
          {
            ""id"": 53461053,
            ""workHourId"": 39609791,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""54. Ерик Најт, Леси се враћа кући"",
            ""workdayDate"": ""2019-11-15""
          },
          {
            ""id"": 53460244,
            ""workHourId"": 39609379,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""53. Изражајно читање прозних текстова"",
            ""workdayDate"": ""2019-11-14""
          },
          {
            ""id"": 53459437,
            ""workHourId"": 39608715,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""52. Придеви"",
            ""workdayDate"": ""2019-11-13""
          },
          {
            ""id"": 37401972,
            ""workHourId"": 27304222,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""7. Синтаксичке и стилске вежбе"",
            ""workdayDate"": ""2019-09-10""
          },
          {
            ""id"": 37380179,
            ""workHourId"": 27035590,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""6. Јанко Веселиновић: Град"",
            ""workdayDate"": ""2019-09-09""
          }
        ]
      },
      ""3"": {
        ""statusId"": 3,
        ""name"": ""неоправдан"",
        ""absents"": [
          {
            ""id"": 60439176,
            ""workHourId"": 43301797,
            ""teacherNote"": null,
            ""statusName"": ""неоправдан"",
            ""workHourNote"": ""72. Читање и анализа домаћег задатка: Необично писмо"",
            ""workdayDate"": ""2019-12-11""
          },
          {
            ""id"": 53461053,
            ""workHourId"": 39609791,
            ""teacherNote"": null,
            ""statusName"": ""неоправдан"",
            ""workHourNote"": ""54. Ерик Најт, Леси се враћа кући"",
            ""workdayDate"": ""2019-11-15""
          }
        ]
      }
    }
  },
  ""2_1_0"": {
    ""classCourseId"": 1092382,
    ""name"": ""Енглески језик (1. страни језик)"",
    ""sequence"": ""2_1_0"",
    ""absentStatuses"": {
      ""1"": {
        ""statusId"": 1,
        ""name"": ""нерегулисан"",
        ""absents"": [
          {
            ""id"": 67531941,
            ""workHourId"": 48546553,
            ""teacherNote"": null,
            ""statusName"": ""нерегулисан"",
            ""workHourNote"": ""37 Исправак теста"",
            ""workdayDate"": ""2020-01-20""
          }
        ]
      },
      ""2"": {
        ""statusId"": 2,
        ""name"": ""оправдан"",
        ""absents"": [
          {
            ""id"": 59408021,
            ""workHourId"": 43575599,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""29.Unit 6- Lessons 5&6- Developing, listening, reading,speaking and writing skills- слушање,читање, говорне вежбе и вежбе писања"",
            ""workdayDate"": ""2019-12-11""
          },
          {
            ""id"": 51900744,
            ""workHourId"": 38477345,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""21. Unit 4 - Lessons 5 and 6 -Developing listening, reading, speaking and writing skills.\n                                                   Развијамо способности слушања, говора ,читања и писања."",
            ""workdayDate"": ""2019-11-13""
          },
          {
            ""id"": 37380295,
            ""workHourId"": 27112434,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""3. Starter unit -Lessons 3 and 4  Поздрави, дани у недељи , обнавњаље вокабулара везаног за играчке и обнављање бројева."",
            ""workdayDate"": ""2019-09-09""
          }
        ]
      }
    }
  },
  ""3_1_0"": {
    ""classCourseId"": 1092277,
    ""name"": ""Математика"",
    ""sequence"": ""3_1_0"",
    ""absentStatuses"": {
      ""1"": {
        ""statusId"": 1,
        ""name"": ""нерегулисан"",
        ""absents"": [
          {
            ""id"": 67634903,
            ""workHourId"": 48584065,
            ""teacherNote"": null,
            ""statusName"": ""нерегулисан"",
            ""workHourNote"": ""92. Дељење вишецифреног броја једноцифреним бројем"",
            ""workdayDate"": ""2020-01-20""
          }
        ]
      },
      ""2"": {
        ""statusId"": 2,
        ""name"": ""оправдан"",
        ""absents"": [
          {
            ""id"": 60439161,
            ""workHourId"": 43302875,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""72.  Једначине са сабирањем и одузимањем"",
            ""workdayDate"": ""2019-12-11""
          },
          {
            ""id"": 53455810,
            ""workHourId"": 39605981,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""54. Јединице мере за површину веће од m2"",
            ""workdayDate"": ""2019-11-15""
          },
          {
            ""id"": 53455315,
            ""workHourId"": 39605628,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""53. Јединице мере за површину m2, dm2,cm2, mm2"",
            ""workdayDate"": ""2019-11-14""
          },
          {
            ""id"": 53454250,
            ""workHourId"": 39604753,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""52.  Јединице мере за површину m2, dm2,cm2, mm2"",
            ""workdayDate"": ""2019-11-13""
          },
          {
            ""id"": 37400180,
            ""workHourId"": 27302211,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""7. Бројеви до десет хиљада. Читање и писање бројева до десет хиљада"",
            ""workdayDate"": ""2019-09-10""
          },
          {
            ""id"": 37380208,
            ""workHourId"": 27035899,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""6. Хиљаде до десет хиљада. Упоређивање хиљада"",
            ""workdayDate"": ""2019-09-09""
          }
        ]
      }
    }
  },
  ""4_1_0"": {
    ""classCourseId"": 1092294,
    ""name"": ""Природа и друштво"",
    ""sequence"": ""4_1_0"",
    ""absentStatuses"": {
      ""2"": {
        ""statusId"": 2,
        ""name"": ""оправдан"",
        ""absents"": [
          {
            ""id"": 53466033,
            ""workHourId"": 39613341,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""22. Настанак српске државе и династија Немањића"",
            ""workdayDate"": ""2019-11-14""
          },
          {
            ""id"": 37403272,
            ""workHourId"": 27305501,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""3. Становништво Србије"",
            ""workdayDate"": ""2019-09-10""
          }
        ]
      }
    }
  },
  ""5_1_0"": {
    ""classCourseId"": 1092449,
    ""name"": ""Ликовна култура"",
    ""sequence"": ""5_1_0"",
    ""absentStatuses"": {
      ""2"": {
        ""statusId"": 2,
        ""name"": ""оправдан"",
        ""absents"": [
          {
            ""id"": 53472543,
            ""workHourId"": 39618058,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""21-22. Колаж и деколаж – Које су боје моја осећања"",
            ""workdayDate"": ""2019-11-15""
          },
          {
            ""id"": 53472544,
            ""workHourId"": 39618060,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""21-22. Колаж и деколаж – Које су боје моја осећања"",
            ""workdayDate"": ""2019-11-15""
          }
        ]
      }
    }
  },
  ""6_1_0"": {
    ""classCourseId"": 1092467,
    ""name"": ""Музичка култура"",
    ""sequence"": ""6_1_0"",
    ""absentStatuses"": {
      ""2"": {
        ""statusId"": 2,
        ""name"": ""оправдан"",
        ""absents"": [
          {
            ""id"": 37380242,
            ""workHourId"": 27036594,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""2. „Моја диридика”,   народна \n „Адађо”,  Томазо Албинони Темпо"",
            ""workdayDate"": ""2019-09-09""
          }
        ]
      }
    }
  },
  ""7_1_0"": {
    ""classCourseId"": 1092505,
    ""name"": ""Физичко васпитање"",
    ""sequence"": ""7_1_0"",
    ""absentStatuses"": {
      ""2"": {
        ""statusId"": 2,
        ""name"": ""оправдан"",
        ""absents"": [
          {
            ""id"": 60439104,
            ""workHourId"": 43348291,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""43. Српско коло „Моравац“"",
            ""workdayDate"": ""2019-12-11""
          },
          {
            ""id"": 53468818,
            ""workHourId"": 39615397,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""32.\tСтав о шакама"",
            ""workdayDate"": ""2019-11-14""
          },
          {
            ""id"": 53468557,
            ""workHourId"": 39615237,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""31. Основне технике одбојке-игра две екипе"",
            ""workdayDate"": ""2019-11-13""
          },
          {
            ""id"": 37403933,
            ""workHourId"": 27306209,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""4. Додавање лопте разним деловима стопала"",
            ""workdayDate"": ""2019-09-10""
          }
        ]
      }
    }
  },
  ""8_2_0"": {
    ""classCourseId"": 1092632,
    ""name"": ""Чувари природе (изборни)"",
    ""sequence"": ""8_2_0"",
    ""absentStatuses"": {
      ""2"": {
        ""statusId"": 2,
        ""name"": ""оправдан"",
        ""absents"": [
          {
            ""id"": 53473066,
            ""workHourId"": 39618386,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""11. Белоглави суп"",
            ""workdayDate"": ""2019-11-14""
          }
        ]
      }
    }
  },
  ""9_2_0"": {
    ""classCourseId"": 1092710,
    ""name"": ""Грађанско васпитање (изборни)"",
    ""sequence"": ""9_2_0"",
    ""absentStatuses"": {
      ""2"": {
        ""statusId"": 2,
        ""name"": ""оправдан"",
        ""absents"": [
          {
            ""id"": 37380127,
            ""workHourId"": 27034987,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""2. Шта нас чека у четвртом разреду"",
            ""workdayDate"": ""2019-09-09""
          }
        ]
      }
    }
  },
  ""11_1_0"": {
    ""classCourseId"": 1092902,
    ""name"": ""ЧОС"",
    ""sequence"": ""11_1_0"",
    ""absentStatuses"": {
      ""2"": {
        ""statusId"": 2,
        ""name"": ""оправдан"",
        ""absents"": [
          {
            ""id"": 54184439,
            ""workHourId"": 40067851,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""11. Стрес - извори,  утицај и механизми превазилажења"",
            ""workdayDate"": ""2019-11-15""
          },
          {
            ""id"": 39565979,
            ""workHourId"": 28103391,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""3. Безбедно кретање у саобраћају"",
            ""workdayDate"": ""2019-09-20""
          }
        ]
      }
    }
  },
  ""12_12_0"": {
    ""classCourseId"": 1391263,
    ""name"": ""Ликовна култура (слободна наставна активност)"",
    ""sequence"": ""12_12_0"",
    ""absentStatuses"": {
      ""2"": {
        ""statusId"": 2,
        ""name"": ""оправдан"",
        ""absents"": [
          {
            ""id"": 54171094,
            ""workHourId"": 40062942,
            ""teacherNote"": null,
            ""statusName"": ""оправдан"",
            ""workHourNote"": ""11. Опрема радова и постављање изложбе"",
            ""workdayDate"": ""2019-11-13""
          }
        ]
      }
    }
  }
}
";
#else
            return string.Empty;
#endif
        }
    }
}
