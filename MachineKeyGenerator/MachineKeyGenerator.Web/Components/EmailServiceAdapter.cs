using Postal;

namespace MachineKeyGenerator.Web
{
    public class EmailServiceAdapter : IEmailService
    {
        public IEmailService Service { get; private set; }

        public EmailServiceAdapter()
        {
            Service = new EmailService();
        }


        public System.Net.Mail.MailMessage CreateMailMessage(Email email)
        {
            return Service.CreateMailMessage(email);
        }

        public void Send(Email email)
        {
            Service.Send(email);
        }

        public System.Threading.Tasks.Task SendAsync(Email email)
        {
            return Service.SendAsync(email);
        }
    }
}