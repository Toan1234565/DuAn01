using System;
using System.Collections.Generic;

namespace DuAn2811_.Models;

public partial class ChiTietGioHang
{
    public int MaChiTietGioHang { get; set; }

    public int? MaSanPham { get; set; }

    public int? MaGioHang { get; set; }

    public virtual GioHang? MaGioHangNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
