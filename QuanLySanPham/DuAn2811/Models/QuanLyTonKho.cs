using System;
using System.Collections.Generic;

namespace DuAn2811_.Models;

public partial class QuanLyTonKho
{
    public int MaTonKho { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuongTon { get; set; }

    public int? So_Luong_Da_Ban { get; set; }

    public DateOnly? NgayCapNhat { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
