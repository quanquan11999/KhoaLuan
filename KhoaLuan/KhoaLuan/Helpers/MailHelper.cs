using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace KhoaLuan.Helpers
{
	public class MailHelper
	{
		public static void sendMail(string toEmailAddress, string subject, string content)
		{
			var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
			var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
			var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
			var stmpHost = ConfigurationManager.AppSettings["STMPHost"].ToString();
			var stmpPort = ConfigurationManager.AppSettings["STMPPort"].ToString();

			bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
			string body = content;

			MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
			message.Subject = subject;
			message.IsBodyHtml = true;
			message.Body = body;

			var client = new SmtpClient();
			client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
			client.Host = stmpHost;
			client.EnableSsl = enableSsl;
			client.Port = !string.IsNullOrEmpty(stmpPort) ? Convert.ToInt32(stmpPort) : 0;
			client.Send(message);
		}
	}
}