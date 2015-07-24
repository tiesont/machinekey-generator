using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MachineKeyGenerator.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{folder}/{*pathInfo}", new { folder = "content" });
            routes.IgnoreRoute("{folder}/{*pathInfo}", new { folder = "scripts" });
            routes.IgnoreRoute("{folder}/{*pathInfo}", new { folder = "fonts" });
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                name: "Errors",
                url: "error/{action}/{id}",
                defaults: new { controller = "Error", action = "General", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DefaultSans",
                url: "{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
