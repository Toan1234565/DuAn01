using System;
using System.Collections.Generic;

namespace DuAn2811_.Models;

public partial class ChiTietKhuyenMai
{
    public int MaChiTietKhuyenMai { get; set; }

    public int? MaSanPham { get; set; }

    public int? MaKhuyenMai { get; set; }

    public virtual KhuyenMai? MaKhuyenMaiNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
