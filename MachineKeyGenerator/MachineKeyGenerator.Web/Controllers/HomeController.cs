using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
//
using MachineKeyGenerator.Helpers;
//
using Postal;

namespace MachineKeyGenerator.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ILogger logger, IEmailService mailer)
            : base(logger, mailer)
        {
        }

        [OutputCache(Duration=0, NoStore=true, VaryByParam="None")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int? id)
        {
            var model = new Models.GeneratedKeyModel();

            try
            {
                var generator = new KeyGenerator();

                /* 512 bits = 64 bytes (512 / 8) */
                model.ValidationKey = generator.GenerateKey(64);

                /* 256 bits = 32 bytes (256 / 8) */
                model.DecryptionKey = generator.GenerateKey(32);

                using (var file = System.IO.File.OpenText(Server.MapPath("~/templates/machinekey.txt")))
                {
                    model.MachineKeyTemplate = string.Format(file.ReadToEnd(), model.ValidationKey, model.DecryptionKey);
                };
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return View(model);
        }


        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(Models.ContactFormModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string ip = Request.UserHostAddress,
                           agent = Request.UserAgent;

                    var helper = new AkismetHelper(AppSettings.AkismetApiKey, AppSettings.AkismetRegisteredSite);

                    var errors = await helper.ProcessFormAsync(model.Name, model.Email, model.Message, "contact-form", ip, agent);

                    if (errors == null || errors.Count() == 0)
                    {
                        try
                        {
                            if (!helper.IsSpam)
                            {
                                string name = HtmlUtility.WhitewashMarkup(model.Name);
                                string email = HtmlUtility.WhitewashMarkup(model.Email);
                                string message = HtmlUtility.SanitizeReduceMarkup(model.Message);

                                string mailHtml = string.Format("<h4>Name:</h4>\n<p>{0}</p>\n<h4>Email:</h4>\n<p>{1}</p>\n<h4>Message:</h4>\n<div>{2}</div>", name, email, message);
                                string mailplaintext = string.Format("Name: {0}\nEmail: {1}\nMessage: {2}", name, email, message);

                                await SendEmailAsync("ContactNotification", AppSettings.ContactEmail, "MKG Contact Notification", mailHtml, mailplaintext);
                            }
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }

                        TempData.Clear();
                        TempData.Add(Alert.Success, "<b>Thank you!</b> Your message was received. You should hear from me shortly.");

                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        foreach (var err in errors)
                        {
                            ModelState.AddModelError("", err);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            // If we're still here, something is invalid or an error has occurred. Redisplay the form.

            return View(model);
        }


        public ActionResult TermsOfUse()
        {
            return View();
        }


        public ActionResult Privacy()
        {
            return View();
        }


        public ActionResult License()
        {
            return View();
        }
    }
}