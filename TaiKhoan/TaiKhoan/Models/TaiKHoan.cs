using System;
using System.Collections.Generic;

namespace TaiKhoan.Models;

public partial class TaiKHoan
{
    public int MaTk { get; set; }

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public string? TenDangNhap { get; set; }

    public string? MatKhau { get; set; }

    public bool? IsEmailConfirmed { get; set; }

    public string? EmailToken { get; set; }

    public virtual ICollection<DanhSachYeuThich> DanhSachYeuThiches { get; set; } = new List<DanhSachYeuThich>();

    public virtual ICollection<DiaChiGiaoHang> DiaChiGiaoHangs { get; set; } = new List<DiaChiGiaoHang>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();
}
