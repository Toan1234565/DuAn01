using System;
using System.Collections.Generic;

namespace DonHang.Models;

public partial class ChiTietXuatKho
{
    public int MaChiTietXuatKho { get; set; }

    public int? MaXuatKho { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuongXuat { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }

    public virtual XuatKho? MaXuatKhoNavigation { get; set; }
}
