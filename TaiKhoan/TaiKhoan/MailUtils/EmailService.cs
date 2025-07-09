using Google.Apis.Auth.OAuth2;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit; // Thêm chỉ thị này
using System;
using System.Threading.Tasks;



namespace TaiKhoan.MailUtils
{
    public class EmailService : IEmailService
    {
        private readonly string _clientId = "840851254934-iqbcjjv8ln1ms7j13golob4rc0hnmg4o.apps.googleusercontent.com";
        private readonly string _clientSecret = "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";
        private readonly string _emailAddress = "nguuentoanbs2k4@gmail.com";

        public async Task SendConfirmationEmailAsync(string email, string token)
        {
            try
            {
                var confirmationLink = $"https://yourdomain.com/Account/ConfirmEmail?token={token}";
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Your Name", _emailAddress));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Xác Thực Email";
                message.Body = new TextPart("html")
                {
                    Text = $"Vui lòng nhấp vào liên kết sau để xác thực email của bạn: <a href='{confirmationLink}'>Xác Thực Email</a>"
                };

                // Kiểm tra sự tồn tại của file JSON
                if (!File.Exists("config/OAuth.json"))
                {
                    throw new FileNotFoundException("OAuth configuration file not found.");
                }

                GoogleCredential credential;
                try
                {
                    credential = GoogleCredential.FromFile("config/OAuth.json")
                        .CreateScoped("https://www.googleapis.com/auth/gmail.send");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error creating Google credential.", ex);
                }

                string accessToken;
                try
                {
                    accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error retrieving access token.", ex);
                }

                // Kết nối SMTP
                using (var client = new SmtpClient())
                {
                    try
                    {
                        await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                        await client.AuthenticateAsync(new SaslMechanismOAuth2(_emailAddress, accessToken));
                        await client.SendAsync(message);
                    }
                    catch (SmtpCommandException smtpEx)
                    {
                        throw new InvalidOperationException($"SMTP command error: {smtpEx.Message}", smtpEx);
                    }
                    catch (SmtpProtocolException protoEx)
                    {
                        throw new InvalidOperationException($"SMTP protocol error: {protoEx.Message}", protoEx);
                    }
                    finally
                    {
                        await client.DisconnectAsync(true);
                    }
                }
            }
            catch (FileNotFoundException fnfEx)
            {
                Console.WriteLine($"File error: {fnfEx.Message}");
            }
            catch (InvalidOperationException invOpEx)
            {
                Console.WriteLine($"Invalid operation: {invOpEx.Message}");
            }
            catch (Exception ex)
            {
                // Bắt các lỗi chung
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
