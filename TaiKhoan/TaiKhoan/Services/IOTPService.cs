namespace TaiKhoan
{
    public interface IOTPService
    {
        string GenerateOtp();
        Task SendOtp(string phoneNumber, string otp);
    }

    public class OtpService : IOTPService
    {
        public string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public async Task SendOtp(string phoneNumber, string otp)
        {
            // Gửi OTP thông qua dịch vụ SMS (Twilio, Nexmo, ...)
        }
    }

}
