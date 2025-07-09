using System;
using System.Collections.Generic;

namespace TaiKhoan.Models;

public partial class ChiTietSanPham
{
    public int MaChiTiet { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuong { get; set; }

    public decimal? Gia { get; set; }

    public string? LoaiSanPham { get; set; }

    public string? ThuocTinh { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
