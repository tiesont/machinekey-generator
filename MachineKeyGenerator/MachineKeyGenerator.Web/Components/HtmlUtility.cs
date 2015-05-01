using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
//
using Ganss.XSS;

namespace MachineKeyGenerator.Helpers
{
    public class HtmlUtility
    {
        private static HtmlSanitizer __sanitizer;

        private static HtmlSanitizer cleaner
        {
            get
            {
                if (__sanitizer == null)
                {
                    __sanitizer = new HtmlSanitizer();
                }

                return __sanitizer;
            }
        }

        public IEnumerable<string> AllowedTags
        {
            get
            {
                return cleaner.AllowedTags;
            }
        }

        public IEnumerable<string> AllowedAttributes
        {
            get
            {
                return cleaner.AllowedAttributes;
            }
        }

        public static string Sanitize(string markup)
        {
            if (!string.IsNullOrWhiteSpace(markup))
            {
                markup = cleaner.Sanitize(markup);
            }

            return markup;
        }

        public static string SanitizeReduceMarkup(string markup)
        {
            if (!string.IsNullOrWhiteSpace(markup))
            {
                var sanitizer = new HtmlSanitizer(allowedTags: new string[] { "b", "i", "u", "em", "strong", "q" });
                markup = sanitizer.Sanitize(markup);
            }

            return markup;
        }

        public static string WhitewashMarkup(string markup)
        {
            if (!string.IsNullOrWhiteSpace(markup))
            {
                var sanitizer = new HtmlSanitizer(allowedTags: new string[] { string.Empty });
                markup = sanitizer.Sanitize(markup);
            }

            return markup;
        }

        public static string CleanUrl(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            // replace hyphens to spaces, remove all leading and trailing whitespace
            value = value.Replace("-", " ").Trim().ToLower();

            // replace multiple whitespace to one hyphen
            value = Regex.Replace(value, @"[\s]+", "-");

            // replace umlauts and eszett with their equivalent
            value = value.Replace("ß", "ss");
            value = value.Replace("ä", "ae");
            value = value.Replace("ö", "oe");
            value = value.Replace("ü", "ue");

            // removes diacritic marks (often called accent marks) from characters
            value = RemoveDiacritics(value);

            // remove all left unwanted chars (white list)
            value = Regex.Replace(value, @"[^a-z0-9\s-]", String.Empty);

            return value;
        }

        public static string RemoveDiacritics(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var sb = new StringBuilder();
            var normalized = value.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);

            foreach (char c in normalized)
            {
                sb.Append(c);
            }

            Encoding nonunicode = Encoding.GetEncoding(850);
            Encoding unicode = Encoding.Unicode;

            byte[] nonunicodeBytes = Encoding.Convert(unicode, nonunicode, unicode.GetBytes(sb.ToString()));
            char[] nonunicodeChars = new char[nonunicode.GetCharCount(nonunicodeBytes, 0, nonunicodeBytes.Length)];
            nonunicode.GetChars(nonunicodeBytes, 0, nonunicodeBytes.Length, nonunicodeChars, 0);

            return new string(nonunicodeChars);
        }

        public static string GetPreviewSnippet(string content)
        {
            var doc = CsQuery.CQ.Create(content);
            string html = doc["p"].Eq(0).Html();

            return html;
        }

        public static string GetSnippet(string content, string selector)
        {
            var doc = CsQuery.CQ.Create(content);
            string html = doc[selector].Eq(0).Html();

            return html;
        }

        public static string GetSnippet(string content, string selector, int limit = 5)
        {
            var builder = new StringBuilder();
            var doc = CsQuery.CQ.Create(content);

            var html = doc[selector].Take(limit).Select(x => x.InnerHTML);
            foreach (var node in html)
            {
                builder.AppendFormat("<{0}>{1}</{2}>", selector, node, selector);
            }

            return builder.ToString();
        }
    }
}
