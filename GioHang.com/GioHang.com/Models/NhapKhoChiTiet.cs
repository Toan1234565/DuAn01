using System;
using System.Collections.Generic;

namespace GioHang.com.Models;

public partial class NhapKhoChiTiet
{
    public int NhapChiTiet { get; set; }

    public int? MaNhapKho { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuong { get; set; }

    public virtual NhapKho? MaNhapKhoNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
