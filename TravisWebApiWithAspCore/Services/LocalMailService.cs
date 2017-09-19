using System;
using System.Diagnostics;
namespace TravisWebApiWithAspCore.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = "travisle247@hotmail.com";
        private string _mailfrom = "travisnoreply@hotmail.com";

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {_mailfrom} to {_mailTo} with localmailservice");
            Debug.WriteLine($"Subject : {subject}");
            Debug.WriteLine($"Message  : {message}");
        }
    }
}
