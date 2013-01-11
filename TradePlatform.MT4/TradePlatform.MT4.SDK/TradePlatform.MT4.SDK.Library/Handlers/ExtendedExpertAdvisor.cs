namespace TradePlatform.MT4.SDK.Library.Handlers
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net.Mail;
    using TradePlatform.MT4.Core;

    public abstract class ExtendedExpertAdvisor : ExpertAdvisor
    {
        protected override string ResolveMethod(string methodName, List<string> parameters)
        {
            switch (methodName)
            {
                case "SendMail":
                    this.SendMail(parameters[0], parameters[1]);
                    return "";
                default:
                    return base.ResolveMethod(methodName, parameters);
            }
        }

        protected void SendMail(string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(ConfigurationManager.AppSettings["NotifyToEmail"]));
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
    }
}
