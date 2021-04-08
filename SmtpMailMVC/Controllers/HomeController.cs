using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmtpMailMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SmtpMailMVC.Controllers
{
    public class HomeController : Controller
    {

            private readonly ILogger<HomeController> _logger;

            public HomeController(ILogger<HomeController> logger)
            {
                _logger = logger;
            }

            public IActionResult Index()
            {
                return View();
            }

            public IActionResult Privacy()
            {
                return View();
            }
            [HttpGet]
            public IActionResult ContactUs()
            {
                return View();
            }
            [HttpPost]
            public IActionResult ContactUs(SendMailTo sendMailTo)
            {
                if (!ModelState.IsValid)
                    return View();
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("jasminsmtp@gmail.com");
                    mail.To.Add("jasminneziric@gmail.com");

                    mail.Subject = sendMailTo.Subject;

                    mail.IsBodyHtml = true;

                    string content = "Name : " + sendMailTo.Name;
                    content += "<br/>Message : " + sendMailTo.Message;

                    mail.Body = content;


                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                    NetworkCredential networkCredential = new NetworkCredential("jasminsmtp@gmail.com", "pw");//Add password
                    //smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = networkCredential;
                    //smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mail);

                    ViewBag.Message = "Mail Send";

                    ModelState.Clear();


                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message.ToString();
                }
                return View();
            }


            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
};
