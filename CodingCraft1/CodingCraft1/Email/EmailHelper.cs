using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CodingCraft1.Email
{
    public class EmailHelper
    {
        public static void SendEmail(string subject, string msg, string to)
        {
            var mail = new MailMessage("codingcraft01@gmail.com", to);
            var client = new SmtpClient
            {
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Credentials = new NetworkCredential("codingcraft01@gmail.com", "coding12345")
            };

            mail.Subject = subject;
            mail.Body = msg;
            client.Send(mail);
        }
    }
}
