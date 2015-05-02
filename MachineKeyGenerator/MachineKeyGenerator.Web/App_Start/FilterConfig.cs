using System.Web.Mvc;

namespace MachineKeyGenerator.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new Security.RequireSecureConnectionAttribute());
        }
    }
}
