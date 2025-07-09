namespace TaiKhoan.MailUtils
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string token);
    }
}
