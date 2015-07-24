using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace MachineKeyGenerator.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {   
        private void BootstrapContainer()
        {
            var container = new Container();

            container.RegisterPackages();

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            System.Web.Mvc.DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        protected void Application_Start()
        {
            // We don't need no stinkin' ASPX pages...
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            BootstrapContainer();

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;

            Response.Clear();
            Server.ClearError();

            var routeData = new RouteData();

            routeData.Values["controller"] = "error";
            routeData.Values["action"] = "general";
            routeData.Values["exception"] = exception;
            Response.StatusCode = 500;

            if (httpException != null)
            {
                Response.StatusCode = httpException.GetHttpCode();
                switch (Response.StatusCode)
                {
                    case 400:
                        routeData.Values["action"] = "badrequest";
                        break;
                    case 401:
                        routeData.Values["action"] = "unauthorized";
                        break;
                    case 403:
                        routeData.Values["action"] = "forbidden";
                        break;
                    case 404:
                        routeData.Values["action"] = "notfound";
                        break;
                    default:
                        routeData.Values["action"] = "general";
                        break;
                }
            }

            // Avoid IIS7 getting in the middle
            Response.TrySkipIisCustomErrors = true;

            IController errorsController = DependencyResolver.Current.GetService<Controllers.ErrorController>();
            HttpContextBase wrapper = new HttpContextWrapper(Context);

            var rc = new RequestContext(wrapper, routeData);
            errorsController.Execute(rc);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
        }


        protected void ErrorLog_Filtering(object sender, Elmah.ExceptionFilterEventArgs e)
        {
            FilterError(e);
        }

        protected void ErrorMail_Filtering(object sender, Elmah.ExceptionFilterEventArgs e)
        {
            FilterError(e);
        }

        private void FilterError(Elmah.ExceptionFilterEventArgs e)
        {
            var ex = e.Exception.GetBaseException() as HttpException;

            //  Dismiss 404 errors for ELMAH
            if (ex != null && ex.GetHttpCode() == 404)
            {
                e.Dismiss();
            }
        }
    }
}
