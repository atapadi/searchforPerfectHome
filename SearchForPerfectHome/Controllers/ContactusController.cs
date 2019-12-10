using SearchForPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace SearchForPerfectHome.Controllers
{
    public class ContactusController : Controller
    {
        // GET: Contactus
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactusViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(vm.Email);
                    msg.To.Add(new MailAddress("searchforperfecthome@gmail.com"));
                    msg.Subject = vm.Subject;

                    string userMessage = "";
                    userMessage = "<br/>Name :" + vm.Name;
                    userMessage = userMessage + "<br>Email Id: " + vm.Email;
                    userMessage = userMessage + "<br/>Subject: " + vm.Subject;
                    userMessage = userMessage + "<br/>Message: " + vm.Message;
                    msg.Body = "Hi, <br/><br/> A new enquiry by user. Detail is as follows:<br/><br/> " + userMessage + "<br/><br/>Thanks";
                    msg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("searchforperfecthome@gmail.com", "Qwert12345.");
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(msg);
                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting us ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                }
            }
            return View();
        }
    }
}