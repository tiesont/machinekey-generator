using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//
using Postal;

namespace MachineKeyGenerator.Web
{
    public class BaseController : Controller
    {
        public ILogger Logger
        {
            get;
            private set;
        }

        public IEmailService Mailer
        {
            get;
            private set;
        }

        public BaseController(ILogger logger)
        {
            Logger = logger;
        }

        public BaseController(ILogger logger, IEmailService mailer)
        {
            Logger = logger;
            Mailer = mailer;
        }


        protected bool IsDebugging 
        {
            get
            {
                return HttpContext.IsDebuggingEnabled;
            }
        }


        protected void SetAlert(string key, string message)
        {
            if (TempData.ContainsKey(key))
            {
                TempData.Remove(key);
            }
            TempData.Add(key, message);
        }

        protected void HandleException(Exception exception, string message = "An error has occurred")
        {
            Logger.LogException(exception);

            TempData.Remove(Alert.Error);
            TempData.Add(Alert.Error, IsDebugging ? exception.Message : message);
        }

        protected void LogException(Exception exception)
        {
            Logger.LogException(exception);
        }


        protected async Task HandleExceptionAsync(Exception exception, string message = "An error has occurred")
        {
            await Logger.LogExceptionAsync(exception);

            TempData.Remove(Alert.Error);
            TempData.Add(Alert.Error, IsDebugging ? exception.Message : message);
        }


        protected void SendEmail(string templateKey, string email, string subject, string htmlMessage, string plainTextMessage)
        {
            if (Mailer == null)
            {
                throw new NullMailerException();
            }

            dynamic message = new Postal.Email(templateKey);
            message.To = email;
            message.HtmlMessage = htmlMessage;
            message.PlainTextMessage = plainTextMessage;

            message.Send();
        }

        protected async Task SendEmailAsync(string templateKey, string email, string subject, string htmlMessage, string plainTextMessage)
        {
            if (Mailer == null)
            {
                throw new NullMailerException();
            }

            dynamic message = new Postal.Email(templateKey);
            message.To = email;
            message.HtmlMessage = htmlMessage;
            message.PlainTextMessage = plainTextMessage;

            await Mailer.SendAsync(message);
        }


        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("index", "home", new { area = string.Empty });
            }
        }


        protected override void HandleUnknownAction(string actionName)
        {
            // Avoid IIS7 getting in the middle
            Response.TrySkipIisCustomErrors = true;

            IController errorsController = DependencyResolver.Current.GetService<Controllers.ErrorController>();
            var errorRoute = new RouteData();
            errorRoute.Values.Add("controller", "error");
            errorRoute.Values.Add("action", "notfound");
            errorRoute.Values.Add("url", HttpContext.Request.Url.OriginalString);
            errorsController.Execute(new RequestContext(HttpContext, errorRoute));
        }
    }
}