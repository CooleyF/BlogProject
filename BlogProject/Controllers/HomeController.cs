using BlogProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult>ContactAsync(EmailModel email)
        {
            //String Interpolation 
            var from = $"{email.FromEmail}<{WebConfigurationManager.AppSettings["emailfrom"]}>";
            var emailMessage = new MailMessage(email.FromEmail, WebConfigurationManager.AppSettings["emailto"])
            {
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true
            };
            var svc = new PersonalEmail();
            await svc.SendAsync(emailMessage);
            return RedirectToAction("Home", "Index");
        }
    }
}