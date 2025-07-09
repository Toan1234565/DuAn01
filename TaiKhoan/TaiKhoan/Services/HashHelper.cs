using System.Security.Cryptography;
using System.Text;
namespace TaiKhoan
{
    public class HashHelper
    {
        public static string HashPassword(string password)
        {
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password); // mã hóa chuỗi mật khẩu thành mảng byte. đây là chuẩn bị câfn thiết để thực hiện băm
                byte[] hash = sha256.ComputeHash(bytes); // của SHA-256 được gọi để băm mảng byte. Kết quả là một mảng byte khác (hash), chứa giá trị băm của mật khẩu.

                StringBuilder resule = new StringBuilder();//được sử dụng để tạo một chuỗi mới từ mảng byte băm (hash).

                foreach (byte b in hash)
                {
                    resule.Append(b.ToString("x2"));//chuyển mỗi byte thành dạng ký tự hexadecimal (cơ số 16) để dễ lưu trữ hoặc hiển thị.
                }
                return resule.ToString();
            }
        }
    }
}
