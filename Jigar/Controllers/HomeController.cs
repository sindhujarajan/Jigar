using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Jigar.Models;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Configuration;
using System.Text;

namespace Jigar.Controllers
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

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Menu1()
        {
            return View();
        }

        public ActionResult OnlineOrder()
        {
            return View();
        }

        public ActionResult Career()
        {
            return View();
        }

        public JsonResult SendApplicantDetails(Applicant applicantModel)
        {
            string statusCode = "";
            string toAddress = System.Configuration.ConfigurationManager.AppSettings["email"];
            if (applicantModel != null)
            {

                StringBuilder emailBody = new StringBuilder();
                emailBody.Append("<body>");
                emailBody.Append("<table style='width: 720px;border: 1px solid whitesmoke;'>");
                //Name
                emailBody.Append("<tr><td>");
                emailBody.Append("<td style='font-size:large;text-align:left;'>Name");
                emailBody.Append("</td>");
                emailBody.Append("<td style='font-size:large;text-align:left;'>" + applicantModel.Name);
                emailBody.Append("</td>");
                emailBody.Append("</tr>");
                //Mobile
                emailBody.Append("<tr><td>");
                emailBody.Append("<td style='font-size:large;text-align:left;'>Mobile");
                emailBody.Append("</td>");
                emailBody.Append("<td style='font-size:large;text-align:left;'>" + applicantModel.Mobile);
                emailBody.Append("</td>");
                emailBody.Append("</tr>");
                //Email
                emailBody.Append("<tr><td>");
                emailBody.Append("<td style='font-size:large;text-align:left;'>Email");
                emailBody.Append("</td>");
                emailBody.Append("<td style='font-size:large;text-align:left;'>" + applicantModel.email);
                emailBody.Append("</td>");
                emailBody.Append("</tr>");

                //Description
                emailBody.Append("<tr><td>");
                emailBody.Append("<td style='font-size:large;text-align:left;'>Description");
                emailBody.Append("</td>");
                emailBody.Append("<td style='font-size:large;text-align:left;'>" + applicantModel.Description);
                emailBody.Append("</td>");
                emailBody.Append("</tr>");
                emailBody.Append("</table>");


                SmtpClient client = new SmtpClient();
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(toAddress));
                message.From = new MailAddress("system@jigarrose.in");
                message.IsBodyHtml = true;
                message.Body = emailBody.ToString();
                var status = sendMail(message);
                if (status == "email sent")
                {
                    statusCode = "SUCCESS";
                    var successData = new
                    {
                        statusCode = statusCode
                    };

                    var successResult = JsonConvert.SerializeObject(successData, Formatting.None);
                    return Json(successResult, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    statusCode = "ERROR SENDING EMAIL";
                    var errData = new
                    {
                        statusCode = statusCode
                    };

                    var errResult = JsonConvert.SerializeObject(errData, Formatting.None);
                    return Json(errResult, JsonRequestBehavior.AllowGet);
                }


            }

            statusCode = "ERROR";
            var data = new
            {
                statusCode = statusCode
            };

            var result = JsonConvert.SerializeObject(data, Formatting.None);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        string sendMail(MailMessage message)
        {
            try
            {
                int smtpPort = 0;
                string smtpServer = ConfigurationManager.AppSettings["SmtpServer"].ToString();
                int.TryParse(ConfigurationManager.AppSettings["SmtpPort"], out smtpPort);
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.EnableSsl = true;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("sindhujarajan1983@gmail.com", "M@ntralayam3");
                smtpClient.Credentials = credentials;            
                smtpClient.Send(message);
                return "email sent";
            }
            catch (SmtpException ex)
            {
                return ex.InnerException.ToString();
            }
        }
    }
}