using System;
using System.Collections.Generic;

namespace Akismet
{
    /// <summary>
    /// DTO, representing a single comment to be verified by Akismet.
    /// </summary>
    public class AkismetComment
    {
        private Uri permalink;

        /// <summary>
        /// Initializes a new instance of the <see cref="AkismetComment"/> class.
        /// </summary>
        /// <param name="blog">The base URI used for the blog.</param>
        public AkismetComment(Uri blog)
        {
            this.Blog = blog;
            this.permalink = blog;
        }

        /// <summary>
        /// Gets the front page or home URL of the instance making the request. For a blog or wiki this ould be the front page.
        /// </summary>
        /// <value>
        /// The blog.
        /// </value>
        /// TODO-rro: Is Required
        /// <remarks>
        /// Must be a full URI, including http://
        /// </remarks>
        public Uri Blog { get; private set; }

        /// <summary>
        /// Gets or sets the IP address of the comment submitter.
        /// </summary>
        /// TODO-rro: Is Required
        public string UserIp { get; set; }

        /// <summary>
        /// Gets or sets the User Agent fo the web browser submitting the comment.
        /// Typically the HTTP_USER_AGENT cgi variable.
        /// </summary>
        /// <remarks>
        /// Not to be confused with the user agent of your Akismet library.
        /// </remarks>
        /// TODO-rro: Is Required
        public string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the content of the HTTP_REFERER header.
        /// </summary>
        public string Referrer { get; set; }

        /// <summary>
        /// Gets or sets the permalink location of the entry the comment was submitted to.
        /// </summary>
        public string Permalink { get { return this.permalink.AbsolutePath; } set { this.permalink = new Uri(this.Blog, value); } }

        /// <summary>
        /// Gets or sets the type of the comment - one of the <see cref="CommentTypes"/> or a made up value like "registration".
        /// </summary>
        public string CommentType { get; set; }

        /// <summary>
        /// Gets or sets the name submitted with the comment.
        /// </summary>
        public string CommentAuthor { get; set; }

        /// <summary>
        /// Gets or sets the Email address submitted with the comment.
        /// </summary>
        public string CommentAuthorEmail { get; set; }

        /// <summary>
        /// Gets or sets the URL submitted with the comment.
        /// </summary>
        public string CommentAuthorUrl { get; set; }

        /// <summary>
        /// Gets or sets the content that was submitted.
        /// </summary>
        public string CommentContent { get; set; }

        /// <summary>
        /// Creates the key value pairs.
        /// </summary>
        /// <returns>Enumerable of KeyValuePair objects for each property of the comment.</returns>
        internal IEnumerable<KeyValuePair<string, string>> CreateKeyValues()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("blog", this.Blog.ToString()));
            list.Add(new KeyValuePair<string, string>("user_ip", this.UserIp));
            list.Add(new KeyValuePair<string, string>("referrer", this.Referrer));
            list.Add(new KeyValuePair<string, string>("permalink", this.permalink.ToString()));
            list.Add(new KeyValuePair<string, string>("comment_type", this.CommentType));
            list.Add(new KeyValuePair<string, string>("comment_author", this.CommentAuthor));
            list.Add(new KeyValuePair<string, string>("comment_author_email", this.CommentAuthorEmail));
            list.Add(new KeyValuePair<string, string>("comment_author_url", this.CommentAuthorUrl));
            list.Add(new KeyValuePair<string, string>("comment_content", this.CommentContent));

            return list;
        }
    }
}
