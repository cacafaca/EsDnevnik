using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using ProCode.EsDnevnik.Model;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ProCode.EsDnevnik.Service
{
    public class EsDnevnik
    {
        #region Private Properties
        readonly UserCredential userCredential;
        HttpClient client;
        private readonly UriDictionary uriDictionary;
        private bool isLoggedIn;

        // Cached vars.
        private string studentsResponseCache;
        private string timeLineResponseCache;
        private string gradesResponseCache;
        #endregion

        #region Constructors
        public EsDnevnik(UserCredential userCredential)
        {
            isLoggedIn = false;
            if (userCredential != null)
            {
                this.userCredential = userCredential;
                uriDictionary = new UriDictionary();
                client = GetNewClient();
            }
            else
                throw new ArgumentNullException(nameof(userCredential));
        }
        #endregion

        #region Public methods

        public async Task LoginAsync()
        {
            // Get token id, to compose login content.
            string token = await GetTokenAsync();
            string content = $"_token={token}&username={Uri.EscapeDataString(userCredential.GetUsername())}&password={userCredential.GetPassword().ToString()}";
            HttpContent loginContent = new StringContent(content);
            loginContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");   // This is important!

            // Send POST command.
            HttpResponseMessage responseMsg = await client.PostAsync(uriDictionary.GetLoginUri(), loginContent);

            if (responseMsg.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseContent = await responseMsg.Content.ReadAsStringAsync();

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(responseContent);
                var linkNodeAppCss = doc.DocumentNode.SelectNodes("//link/@href")?.Where(node => node.Attributes["href"].Value.StartsWith("/css/app.css"))?.FirstOrDefault();
                if (linkNodeAppCss != null)
                {
                    isLoggedIn = true;
                }
                else
                {
                    throw new LoginException(responseMsg.StatusCode, "Can't log in.");
                }
            }
            else
            {
                throw new LoginException(responseMsg.StatusCode, "Ne mogu da se prijavim na moj.esdnevnik.rs. Greška: " + responseMsg.ReasonPhrase);
            }
        }

        public async Task LogoutAsync()
        {
            HttpResponseMessage responseMsg = await client.GetAsync(uriDictionary.GetLogoutUri());
            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                isLoggedIn = false;
            }
            else
            {
                throw new Exception("Can't logout.");
            }

        }
        public Model.GeneratedTimeLine.Rootobject GetTimeLineEventsFake()
        {
            return new Model.GeneratedTimeLine.Rootobject
            {
                Data = new Dictionary<string, Model.GeneratedTimeLine.TimeLineEvent[]>
                {
                    {   "2019-12-27",
                        new Model.GeneratedTimeLine.TimeLineEvent[]
                        {
                            new Model.GeneratedTimeLine.TimeLineEvent
                            {
                                Type = Model.GeneratedTimeLine.EventType.Absent,
                                Date = "2019-01-01",
                                CreateTime = "2019-01-01",
                                FullGrade = "odlican (5)",
                                Grade = new Model.GeneratedTimeLine.Grade
                                {
                                    Id = 5,
                                    GradeTypeId = 5,
                                    Name = "Odlican",
                                    Value = 5,
                                    Sequence = 1
                                }
                            },
                            new Model.GeneratedTimeLine.TimeLineEvent
                            {
                                Type = Model.GeneratedTimeLine.EventType.Grade,
                                Date = "2019-01-02",
                                CreateTime = "2019-01-01",
                                FullGrade = "odlican (5)",
                                Grade = new Model.GeneratedTimeLine.Grade
                                {
                                    Id = 5,
                                    GradeTypeId = 5,
                                    Name = "Odlican",
                                    Value = 5,
                                    Sequence = 1
                                }
                            }
                        }
                    },
                    {   "2019-12-26",
                        new Model.GeneratedTimeLine.TimeLineEvent[]
                        {
                            new Model.GeneratedTimeLine.TimeLineEvent
                            {
                                Type = Model.GeneratedTimeLine.EventType.FinalGrade,
                                Date = "2019-01-03",
                                CreateTime = "2019-01-01",
                                FullGrade = "dobar (3)",
                                Grade = new Model.GeneratedTimeLine.Grade
                                {
                                    Id = 5,
                                    GradeTypeId = 5,
                                    Name = "Odlican",
                                    Value = 5,
                                    Sequence = 1
                                }
                            }
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Get students for logged parent.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Student>> GetStudentsAsync()
        {
            IList<Student> students = new List<Student>();

            HttpResponseMessage responseMsg = await client.GetAsync(uriDictionary.GetStudentsUri());
            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                studentsResponseCache = await responseMsg.Content.ReadAsStringAsync();
                var studentsResponseObj = Newtonsoft.Json.Linq.JObject.Parse(studentsResponseCache);
                var data = studentsResponseObj.SelectToken("$.data", true);
                foreach (var studentToken in data.Children())
                {
                    Student newStudent = new Student()
                    {
                        Id = int.Parse(studentToken["id"].ToString()),
                        FullName = studentToken["fullName"].ToString(),
                        Jmbg = studentToken["jmbg"].ToString(),
                        Gender = studentToken["gender"].ToString()
                    };
                    foreach (var school in studentToken["schools"].Children())
                    {
                        if (school is JProperty schoolProp)
                        {
                            var newSchool = new School()
                            {
                                Id = int.Parse(schoolProp.Name),
                                SchoolName = schoolProp.Value["schoolName"]?.ToString()
                            };
                            foreach (var schoolYearToken in schoolProp.Value["schoolyears"].Children())
                            {
                                if (schoolYearToken is JProperty schoolYearProp)
                                {
                                    var newSchoolYear = new SchoolYear
                                    {
                                        Id = int.Parse(schoolYearProp.Value["year_id"].ToString()),
                                        Year = schoolYearProp.Value["year_id"].ToString()
                                    };

                                    foreach (var classToken in schoolYearProp.Value["classes"].Children())
                                    {
                                        if (classToken is JProperty classProp)
                                        {
                                            var newClass = new Class
                                            {
                                                RecordId = int.Parse(classProp.Name),
                                                Section = classProp.Value["section"].ToString(),
                                                StudentClassId = int.Parse(classProp.Value["studentClassId"].ToString())
                                            };
                                            newSchoolYear.Classes.Add(newClass);
                                        }
                                    }

                                    newSchool.SchoolYears.Add(newSchoolYear);
                                }
                            }
                            newStudent.Schools.Add(newSchool);
                        }
                    }
                    students.Add(newStudent);
                }
            }
            else
            {
                throw new Exception("Can't read students info.");
            }

            return students;
        }

        /// <summary>
        ///  For testing. Return two students.        
        /// </summary>
        /// <returns></returns>
        public IList<Student> GetStudentsFake()
        {
            IList<Student> students = new List<Student>()
            {
                new Student()
                {
                    Id = 11111,
                    FullName = "Petar Petrović",
                    Gender = "m",
                    Jmbg = "0101009710123"
                },
                new Student()
                {
                    Id = 22222,
                    FullName = "Bojana Bojanić",
                    Gender = "f",
                    Jmbg = "3112009710123"
                },
            };
            return students;
        }

        private const int timeLineEventFirstPage = 1;
        private int timeLineEventsPage = timeLineEventFirstPage;

        /// <summary>
        /// Get time line.
        /// </summary>
        /// <returns></returns>
        public async Task<Model.GeneratedTimeLine.Rootobject> GetTimeLineEventsAsync(Student student, bool resetTimleLineEventPage = false)
        {
            if (resetTimleLineEventPage)
                timeLineEventsPage = timeLineEventFirstPage;

            HttpResponseMessage responseMsg = await client.GetAsync(uriDictionary.GetTimeLineEventsUri(student, ref timeLineEventsPage));

            Model.GeneratedTimeLine.Rootobject rootTimeLine;
            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                timeLineEventsPage++;
                timeLineResponseCache = await responseMsg.Content.ReadAsStringAsync();
                rootTimeLine = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.GeneratedTimeLine.Rootobject>(timeLineResponseCache);
            }
            else
            {
                throw new Exception("Can't read time line.");
            }

            return rootTimeLine;
        }

        public async Task<Model.GeneratedGrades.Rootobject> GetGradesAsync(Student student)
        {
            HttpResponseMessage responseMsg = await client.GetAsync(uriDictionary.GetGradesUri(student));
            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                gradesResponseCache = await responseMsg.Content.ReadAsStringAsync();
                return new Model.GeneratedGrades.Rootobject()
                {
                    Grades = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.GeneratedGrades.GradesArray[]>(gradesResponseCache)
                };
            }
            else
                return null;
        }
        public Model.GeneratedGrades.Rootobject GetGradesFake()
        {
            return new Model.GeneratedGrades.Rootobject
            {
                Grades = new Model.GeneratedGrades.GradesArray[]
                {
                    new Model.GeneratedGrades.GradesArray
                    {
                        Course = "Srpski",
                        ClassCourseId = 12345,
                        ClassCourseGradeTypeId = 1,
                        Sequence = 10,
                        Parts = new Model.GeneratedGrades.Parts
                        {
                            Part1Value = new Model.GeneratedGrades.Part1
                            {
                                Grades = new Model.GeneratedGrades.Grade[]
                                {
                                    new Model.GeneratedGrades.Grade
                                    {
                                        Descriptive = false,
                                        Date = "28.12.2019",
                                        CreateTime = "28.12.2019 10:00:00",
                                        FullGrade = "Odlican (5)",
                                        GradeValue = 5,
                                        GradeCategory = "praktican rad",
                                        Note = "Beleska",
                                        SchoolyearPartId = null,
                                        EvaluationElement = null
                                    }
                                }
                            },
                            Part2Value = new Model.GeneratedGrades.Part2
                            {
                                Grades = null,
                                Final = null,
                                Average = 0
                            }
                        }
                    }
                }
            };
        }

        public bool IsLoggedIn()
        {
            return isLoggedIn;
        }
        #endregion

        #region Private methods
        private async Task<string> GetTokenAsync()
        {
            string token = string.Empty;
            HttpResponseMessage msg = await client.GetAsync(uriDictionary.GetLoginUri());
            if (msg.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new LoginException(msg.StatusCode, msg.ReasonPhrase);
            }
            string responseContent = await msg.Content.ReadAsStringAsync();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(responseContent);
            var tokenNodes = doc.DocumentNode.SelectNodes("//input[@name]");
            if (tokenNodes != null && tokenNodes.Count > 0)
            {
                token = tokenNodes.Where(node => node.Attributes.Where(name => name.Name == "name" && name.Value == "_token").Count() > 0).First()?.Attributes["value"].Value;
            }
            else
                throw new LoginException(msg.StatusCode, "Can't find token node.");

            return token;
        }

        private HttpClient GetNewClient()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                CookieContainer = new CookieContainer()
            };
            client = new HttpClient(handler);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "sr,en-US;q=0.7,en;q=0.3");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

            client.BaseAddress = uriDictionary.GetBase();
            client.DefaultRequestHeaders.Host = uriDictionary.GetBase().Host;

            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("EsDnevnik-Unofficial", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()));

            return client;
        }
        #endregion
    }
}
