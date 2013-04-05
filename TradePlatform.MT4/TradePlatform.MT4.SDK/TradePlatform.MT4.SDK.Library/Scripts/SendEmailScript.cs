using System.Configuration;
using System.Net.Mail;
using TradePlatform.MT4.Core;
using TradePlatform.MT4.SDK.API;

namespace TradePlatform.MT4.SDK.Library.Scripts
{
    public class SendEmailScript : ExpertAdvisor
    {
        protected override int Init()
        {
            return 1;
        }

        protected override int Start()
        {
            var mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(ConfigurationManager.AppSettings["NotifyToEmail"]));
            mailMessage.Subject = this.AccountName();
            mailMessage.Body = this.AccountProfit().ToString();

            var smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);

            return 2;
        }

        protected override int DeInit()
        {
            return 3;
        }
    }
}