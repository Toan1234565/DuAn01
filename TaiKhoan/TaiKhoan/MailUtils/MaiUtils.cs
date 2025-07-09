using Stripe.Billing;
using System.Net.Mail;
using static QRCoder.PayloadGenerator;
using System.Net;
using Azure.Core;

namespace TaiKhoan.MailUtils
{
    public class MaiUtils
    {
        public static void SenMai(string from, string to, string subject, string body)
        {
            MailMessage message = new MailMessage(from, to, subject, body);

        }
        
    }
}
