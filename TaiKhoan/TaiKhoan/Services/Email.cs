using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
namespace TaiKhoan
{
    public class Email
    {
        private readonly string _apikey;
        public Email(string apikey)
        {
            _apikey = apikey;
        }
        public async Task SendEmailAsyns(string email, string subject, string message)
        {
            var client = new SendGridClient(_apikey);
            var from = new EmailAddress("nguuentoanbs2k4@gmail.com","ToanNe");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message,message);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
