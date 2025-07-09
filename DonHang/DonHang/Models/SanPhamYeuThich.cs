using System;
using System.Collections.Generic;

namespace DonHang.Models;

public partial class SanPhamYeuThich
{
    public int MaChiTietYeuThich { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuong { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
