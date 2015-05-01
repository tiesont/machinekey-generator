using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//
using Akismet;

namespace MachineKeyGenerator.Web
{
    public class AkismetHelper
    {
        private bool isSpam;
        private readonly AkismetService api;

        public AkismetHelper(string key, string site, string agent = "")
        {
            api = new AkismetService(key, new Uri(site), null);
        }


        public bool IsSpam
        {
            get
            {
                return isSpam;
            }
        }


        public async Task<bool> CanCheckAsync()
        {
            bool verified = await api.VerifyKeyAsync();
            return verified;
        }

        public async Task<string[]> ProcessFormAsync(string name, string email, string message, string type, string ipAddress = "", string userAgent = "")
        {
            var errors = new List<string>();

            if (await CanCheckAsync())
            {
                var comment = api.CreateComment();
                comment.CommentAuthor = name;
                comment.CommentAuthorEmail = email;
                comment.CommentContent = message;
                comment.CommentType = type;

                if (!string.IsNullOrWhiteSpace(userAgent))
                    comment.UserAgent = userAgent;

                if (!string.IsNullOrWhiteSpace(ipAddress))
                    comment.UserIp = ipAddress;

                var check = await api.CheckCommentAsync(comment);
                if (check == CommentCheck.Invalid)
                {
                    errors.Add("Akismet validation failed.");
                }
                else
                {
                    isSpam = check == CommentCheck.Spam;

                    if (isSpam)
                    {
                        errors.Add("Message appears to be spam.");
                    }
                }
            }
            else
            {
                errors.Add("Akismet key seems to be invalid.");
            }

            return errors.ToArray();
        }


        public async Task<string[]> SubmitCommentAsSpam(string key, string site, string name, string email, string message, string type, string ipAddress = "", string userAgent = "")
        {
            var errors = new List<string>();

            if (await CanCheckAsync())
            {
                var comment = api.CreateComment();
                comment.CommentAuthor = name;
                comment.CommentAuthorEmail = email;
                comment.CommentContent = message;
                comment.CommentType = type;

                if (!string.IsNullOrWhiteSpace(userAgent))
                    comment.UserAgent = userAgent;

                if (!string.IsNullOrWhiteSpace(ipAddress))
                    comment.UserIp = ipAddress;

                if (!await api.SubmitSpamAsync(comment))
                {
                    errors.Add("Akismet correction failed.");
                }
            }
            else
            {
                errors.Add("Akismet key seems to be invalid.");
            }

            return errors.ToArray();
        }


        public async Task<string[]> SubmitCommentAsHam(string key, string site, string name, string email, string message, string type, string ipAddress = "", string userAgent = "")
        {
            var errors = new List<string>();

            if (await CanCheckAsync())
            {
                var comment = api.CreateComment();
                comment.CommentAuthor = name;
                comment.CommentAuthorEmail = email;
                comment.CommentContent = message;
                comment.CommentType = type;

                if (!string.IsNullOrWhiteSpace(userAgent))
                    comment.UserAgent = userAgent;

                if (!string.IsNullOrWhiteSpace(ipAddress))
                    comment.UserIp = ipAddress;

                if (!await api.SubmitHamAsync(comment))
                {
                    errors.Add("Akismet correction failed.");
                }
            }
            else
            {
                errors.Add("Akismet key seems to be invalid.");
            }

            return errors.ToArray();
        }
    }
}
