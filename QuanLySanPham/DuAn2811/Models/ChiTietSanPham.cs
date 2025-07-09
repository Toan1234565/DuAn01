using System;
using System.Collections.Generic;

namespace DuAn2811_.Models;

public partial class ChiTietSanPham
{
    public int MaChiTiet { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuong { get; set; }

    public decimal? Gia { get; set; }

    public string? LoaiSanPham { get; set; }

    public string? ThuocTinh { get; set; }

    public string? Mau { get;  set; }

    public string? Dung_Luong { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
