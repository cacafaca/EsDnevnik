using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using ProCode.EsDnevnik.Model;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ProCode.EsDnevnik.Model.GeneratedAbsences;

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
        private readonly IList<string> timeLineResponseCache = new List<string>();
        private string gradesResponseCache;
        private string absencesResponseCache;
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

        /// <summary>
        /// Login to system using username and password.
        /// </summary>
        /// <returns></returns>
        public async Task LoginAsync()
        {
            // Get token id, to compose login content.
            string token = await GetTokenAsync();
            string content = $"_token={token}&username={Uri.EscapeDataString(userCredential.GetUsername())}&password={userCredential.GetPassword()}";
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

        /// <summary>
        /// Logout from the system.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get students for logged parent/user.
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
        private int nextTimeLineEventsPage = timeLineEventFirstPage;

        /// <summary>
        /// Get time line.
        /// </summary>
        /// <returns></returns>
        public async Task<Model.GeneratedTimeLine.Rootobject> GetTimeLineEventsAsync(Student student, bool resetTimleLineEventPage = false)
        {
            if (resetTimleLineEventPage)
            {
                nextTimeLineEventsPage = timeLineEventFirstPage;
                timeLineResponseCache.Clear();
            }

            HttpResponseMessage responseMsg = await client.GetAsync(uriDictionary.GetTimeLineEventsUri(student, ref nextTimeLineEventsPage));

            Model.GeneratedTimeLine.Rootobject rootTimeLine;
            Model.GeneratedTimeLine.Rootobject rootTimeLineSorted = new Model.GeneratedTimeLine.Rootobject();
            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                nextTimeLineEventsPage++;
                timeLineResponseCache.Add(await responseMsg.Content.ReadAsStringAsync());

                // Check if reached end of data                
                var timelineResponseObj = JObject.Parse(timeLineResponseCache.Last());
                var data = timelineResponseObj.SelectToken("$.data", true);
                if (data != null && data.Children().Count() > 0)
                {
                    rootTimeLine = JsonConvert.DeserializeObject<Model.GeneratedTimeLine.Rootobject>(timeLineResponseCache.Last());
                    rootTimeLineSorted.Meta = rootTimeLine.Meta;
                    foreach (var dateItem in rootTimeLine.Data.OrderByDescending(item => item.Key))
                    {
                        List<Model.GeneratedTimeLine.TimeLineEvent> newTimeLineEventList = new List<Model.GeneratedTimeLine.TimeLineEvent>();
                        foreach (var timeLineEvent in dateItem.Value.OrderByDescending(item => item.Date))
                        {
                            timeLineEvent.Note = System.Web.HttpUtility.HtmlDecode(timeLineEvent.Note);
                            newTimeLineEventList.Add(timeLineEvent);
                        }
                        rootTimeLineSorted.Data.Add(dateItem.Key, newTimeLineEventList.ToArray());
                    }
                }
            }
            else
            {
                throw new Exception("Can't read time line.");
            }
            return rootTimeLineSorted;
        }

        public Model.GeneratedTimeLine.Rootobject GetTimeLineEventsFake()
        {
            string timeLineEventsJson = FakeData.GetFakeTimeLineEvents();
            return JsonConvert.DeserializeObject<Model.GeneratedTimeLine.Rootobject>(timeLineEventsJson);
        }

        public async Task<Model.GeneratedGrades.Rootobject> GetGradesAsync(Student student)
        {
            HttpResponseMessage responseMsg = await client.GetAsync(uriDictionary.GetGradesUri(student));
            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                gradesResponseCache = await responseMsg.Content.ReadAsStringAsync();
                return new Model.GeneratedGrades.Rootobject()
                {
                    Courses = JsonConvert.DeserializeObject<Model.GeneratedGrades.CourseGrades[]>(gradesResponseCache)
                };
            }
            else
                return null;
        }
        public Model.GeneratedGrades.Rootobject GetGradesFake()
        {
            string gradesJson = FakeData.GetFakeGradesJson();
            Model.GeneratedGrades.CourseGrades[] courses = JsonConvert.DeserializeObject<Model.GeneratedGrades.CourseGrades[]>(gradesJson);
            return new Model.GeneratedGrades.Rootobject { Courses = courses };
        }

        public bool IsLoggedIn()
        {
            return isLoggedIn;
        }

        public async Task<AbsencesRoot> GetAbsencesAsync(Student student)
        {
            HttpResponseMessage responseMsg = await client.GetAsync(uriDictionary.GetAbsenceUri(student));
            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                absencesResponseCache = await responseMsg.Content.ReadAsStringAsync();
                if (absencesResponseCache.Trim() != "[]")   // Protect from empty lists, until I figure out correct set of structures.
                    return JsonConvert.DeserializeObject<AbsencesRoot>(absencesResponseCache);
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// Returns hard-coded json.
        /// </summary>
        /// <returns></returns>
        public AbsencesRoot GetAbsencesFake()
        {
            string gradesJson = FakeData.GetFakeAbsencesJson();
            return JsonConvert.DeserializeObject<AbsencesRoot>(gradesJson);
        }

        /// <summary>
        /// Send request to reset password.
        /// </summary>
        /// <returns></returns>
        public async Task ResetPasswordRequestAsync()
        {
            // Get token id, to compose login content.
            string token = await GetTokenAsync();
            string content = $"_token={token}&email={Uri.EscapeDataString(userCredential.GetUsername())}";
            HttpContent resetPasswordRequestContent = new StringContent(content);
            resetPasswordRequestContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");   // This is important!

            // Send POST command.
            HttpResponseMessage responseMsg = await client.PostAsync(uriDictionary.GetResetPasswordRequestUri(), resetPasswordRequestContent);

            if (responseMsg.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseContent = await responseMsg.Content.ReadAsStringAsync();
                if (!responseContent.Contains("Захтев за ресетовање лозинке је послат на Ваш имејл."))
                    throw new Exception("Неуспешно слање захтева за поништење лозинке.");
            }
            else
            {
                throw new LoginException(responseMsg.StatusCode, "Не могу да се пријавим на moj.esdnevnik.rs. Грешка: " + responseMsg.ReasonPhrase);
            }
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
