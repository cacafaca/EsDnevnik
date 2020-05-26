using MailKit;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCode.EsDnevnik.Service
{
    public class Email
    {
        public Email(UserCredential userCredential)
        {
            this.userCredential = userCredential;
            client = new MailKit.Net.Imap.ImapClient();
            uriDictionary = new UriDictionary();

        }
        private readonly UserCredential userCredential;
        private readonly MailKit.Net.Imap.ImapClient client;
        private readonly UriDictionary uriDictionary;

        public async Task Login()
        {
            await client.ConnectAsync("imap.gmail.com", 993, MailKit.Security.SecureSocketOptions.SslOnConnect);
            //var oauth2 = new SaslMechanismOAuth2();
            await client.AuthenticateAsync(userCredential.GetUsername(), userCredential.GetPassword());
        }

        public void Logout()
        {
            client.Disconnect(true);
        }

        private IEnumerable<uint> GetResetPasswordRequestMessages()
        {
            IEnumerable<uint> uids = null;
            //if (client.IsAuthenticated)
            //    uids = client.Inbox.Search(
            //            SearchCondition.From("info@esdnevnik.rs").And(
            //            SearchCondition.Subject("Захтев за ресет лозинке")).And(
            //            SearchCondition.SentOn(DateTime.Now.Date)));
            return uids;
        }

        public Uri GetResetPasswordLink()
        {
            Uri resetPasswordUri = null;
            //if (client.Authed)
            //{
            //    var resetPasswordRequestIds = GetResetPasswordRequestMessages();
            //    if (resetPasswordRequestIds != null && resetPasswordRequestIds.Count() > 0)
            //    {
            //        var lastId = resetPasswordRequestIds.Last();
            //        var lastMessage = client.GetMessages(new List<uint> { lastId });
            //        if (lastMessage != null && lastMessage.Count() > 0)
            //        {
            //            string body = lastMessage.First().Body;
            //            int start = body.IndexOf(uriDictionary.GetResetPasswordUri().ToString());
            //            if (start >= 0)
            //            {
            //                int end = body.IndexOf(' ', start);
            //                string extractedLink = body.Substring(start, end-start);
            //                if (!string.IsNullOrWhiteSpace(extractedLink))
            //                {
            //                    resetPasswordUri = new Uri(extractedLink);
            //                }
            //            }
            //        }
            //    }
            //}
            return resetPasswordUri;
        }
    }
}
