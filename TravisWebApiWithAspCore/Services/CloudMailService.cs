using System;
using System.Diagnostics;

namespace TravisWebApiWithAspCore.Services
{
    public class CloudMailService : IMailService
    {
		private string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
		private string _mailfrom = Startup.Configuration["mailSettings:mailFromAddress"];

		public void Send(string subject, string message)
		{
			Debug.WriteLine($"Mail from {_mailfrom} to {_mailTo} with cloudmailservice");
			Debug.WriteLine($"Subject : {subject}");
			Debug.WriteLine($"Message  : {message}");
		}
    }
}
