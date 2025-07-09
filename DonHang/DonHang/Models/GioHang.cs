using System;
using System.Collections.Generic;

namespace DonHang.Models;

public partial class GioHang
{
    public int MaGioHang { get; set; }

    public int? MaTk { get; set; }

    public int? SoLuongSanPham { get; set; }

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual TaiKHoan? MaTkNavigation { get; set; }
}
