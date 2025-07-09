using System.Security.Cryptography;
using System.Text;
namespace TaiKhoan
{
    public class AES_256
    {
        public static string ASE(string palintext, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32)); // dam bao do dai khoa la 256bit (32 byte)
                aes.IV = new byte[16]; // vector khoi tao (IV), su dung gia tri mac dinh cho don gian (tuy chon nang cao co th cau hinh IV)
                using(var encryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    var palainBytes = Encoding.UTF8.GetBytes(palintext);
                    var cipherBytes = encryptor.TransformFinalBlock(palainBytes,0,palainBytes.Length);
                    return Convert.ToBase64String(cipherBytes);
                }
            }
        }
        public static string Decrupt(string cipherText, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
                aes.IV = new byte[16];
                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    var cipherBytes = Convert.FromBase64String(cipherText);
                    var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(plainBytes);
                }
            }
        }
    }
}
