using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace WP.NetCore.Common.Helper
{
    public class EmailHelper
    {
        public static void SendEmail(string title,string body)
        {
            var host = Appsettings.app("ExceptionMail", "Host");
            if (string.IsNullOrWhiteSpace(host))
            {
                return;
            }
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("wpjob", Appsettings.app("ExceptionMail", "UserName")));
            message.To.Add(new MailboxAddress("wpjob", "igmsapp@163.com"));
            var receives = Appsettings.app("ExceptionMail", "Receives").Split(',');
            foreach (var item in receives)
            {
                message.To.Add(new MailboxAddress("", item));
            }
            message.Subject = title;
            message.Body = new TextPart("html")
            {
                Text = body
            };
            using (var client = new SmtpClient())
            {
                client.Connect(host, Appsettings.app("ExceptionMail", "Port").ToInt32(), Appsettings.app("ExceptionMail", "Ssl").ToBool());
                client.Authenticate(Appsettings.app("ExceptionMail", "UserName"), Appsettings.app("ExceptionMail", "PassWord"));
                client.Send(message);
                client.Disconnect(true);
            }

        }
    }
}
