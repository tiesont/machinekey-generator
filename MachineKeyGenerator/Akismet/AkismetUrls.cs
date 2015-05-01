namespace Akismet
{
    /// <summary>
    /// List of URLs specified by Akismet for the different APIs.
    /// </summary>
    internal static class AkismetUrls
    {
        public static string VerifyKey = "/1.1/verify-key";
        public static string ValidateComment = "/1.1/comment-check";
        public static string SubmitSpam = "/1.1/submit-spam";
        public static string SubmitHam = "/1.1/submit-ham";
    }
}
