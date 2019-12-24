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
        
        // Cached vars.
        private string studentsResponseCache;
        private string timeLineResponseCache;
        #endregion

        #region Constructors
        public EsDnevnik(UserCredential userCredential)
        {
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

        public IList<TimeLine> GetTimeLineFake()
        {
            return new List<TimeLine>
            {
                new TimeLine
                {
                    EventDate = new DateTime(2019, 12, 2),
                },
                new TimeLine
                {
                    EventDate = new DateTime(2019, 12, 3),
                },
                new TimeLine
                {
                    EventDate = new DateTime(2019, 12, 10),
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
                    Id = 123456,
                    FullName = "Bojan Bojanić",
                    Gender = "m",
                    Jmbg = "0101009710123"
                },
                new Student()
                {
                    Id = 123456,
                    FullName = "Bojana Bojanić",
                    Gender = "f",
                    Jmbg = "0101009710123"
                },
            };
            return students;
        }

        /// <summary>
        /// Get time line.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<TimeLine>> GetTimeLineAsync()
        {
            IList<TimeLine> timeLine = new List<TimeLine>();

            HttpResponseMessage responseMsg = await client.GetAsync(uriDictionary.GetStudentsUri());
            if (responseMsg.StatusCode == HttpStatusCode.OK)
            {
                timeLineResponseCache = await responseMsg.Content.ReadAsStringAsync();
                var timeLineResponseObj = Newtonsoft.Json.Linq.JObject.Parse(timeLineResponseCache);
                var data = timeLineResponseObj.SelectToken("$.data", true);
                foreach (var timeLineToken in data.Children())
                {

                }
            }
            else
            {
                throw new Exception("Can't read time line.");
            }

            return timeLine;
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
