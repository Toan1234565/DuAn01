using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DuAn2811_.Models;

public partial class TaiKHoan
{
    public int MaTk { get; set; }
    [Required(ErrorMessage ="Email không được để trống ")]
    [EmailAddress(ErrorMessage ="Email không đúng định dạng.")]
    public string? Email { get; set; }

    [Required(ErrorMessage ="Số điện thoại không được để trống.")]
    public string? SoDienThoai { get; set; }
    [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
    public string? TenDangNhap { get; set; }
    [Required(ErrorMessage = "Mật khẩu  không được để trống.")]
    [MinLength(8, ErrorMessage ="")]
    [RegularExpression(@"'(?=.*[a-z])(?=.*[A-Z])(?=.*\W).+$"", ErrorMessage = ""Mật khẩu phải có ít nhất 1 chữ hoa, 1 chữ thường và 1 ký tự đặc biệt.")]
    public string? MatKhau { get; set; }

    public bool IsEmailConfirmed { get; set; } = false; // Mặc định là chưa xác thực
    public string? EmailToken { get; set; } // Mã token để xác thực email

    public virtual ICollection<DanhSachYeuThich> DanhSachYeuThiches { get; set; } = new List<DanhSachYeuThich>();

    public virtual ICollection<DiaChiGiaoHang> DiaChiGiaoHangs { get; set; } = new List<DiaChiGiaoHang>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();
}
