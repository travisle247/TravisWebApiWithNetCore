using System;
namespace TravisWebApiWithAspCore.Services
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}
