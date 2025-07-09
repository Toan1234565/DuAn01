using System;
using System.Collections.Generic;

namespace GioHang.com.Models;

public partial class QuanLyTonKho
{
    public int MaTonKho { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuongTon { get; set; }

    public DateOnly? NgayCapNhat { get; set; }

    public int? SoLuongDaBan { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
