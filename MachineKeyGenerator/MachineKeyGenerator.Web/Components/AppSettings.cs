using System.Configuration;

namespace MachineKeyGenerator.Helpers
{
    public class AppSettings
    {
        public static string Name
        {
            get
            {
                return ConfigurationManager.AppSettings["app.name"];
            }
        }

        public static string Title
        {
            get
            {
                return ConfigurationManager.AppSettings["app.title"];
            }
        }

        public static string Version
        {
            get
            {
                return ConfigurationManager.AppSettings["app.version"];
            }
        }

        public static string CopyrightOwner
        {
            get
            {
                return ConfigurationManager.AppSettings["app.owner"];
            }
        }

        public static string CopyrightOwnerUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["app.ownerUrl"];
            }
        }

        public static string ContactEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["app.contactEmail"];
            }
        }


        public static string AkismetApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["akismet.apiKey"];
            }
        }

        public static string AkismetRegisteredSite
        {
            get
            {
                return ConfigurationManager.AppSettings["akismet.registeredSite"];
            }
        }

        public static string Find(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }


        private static bool GetBoolValue(string key, bool defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (value == null)
                return defaultValue;
            bool returnValue;
            if (!bool.TryParse(value, out returnValue))
                return defaultValue;
            return returnValue;
        }
    }
}
