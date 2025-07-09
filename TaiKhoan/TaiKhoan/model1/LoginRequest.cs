using System.ComponentModel.DataAnnotations;

namespace TaiKhoan.model1
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string MatKhau { get; set; }
    }

}
