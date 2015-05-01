namespace MachineKeyGenerator.Web
{
    public class Alert
    {
        public static string Attention = "alert-attention";
        public static string Success = "alert-success";
        public static string Error = "alert-danger alert-error"; /* To account for differences in BS2 & BS3 */
        public static string Warning = "alert-warning";
        public static string Information = "alert-info";
        public static string Debug = "alert-debug";

        public static string[] All
        {
            get
            {
                return new string[] { Attention, Success, Error, Warning, Information, Debug };
            }
        }
    }
}
