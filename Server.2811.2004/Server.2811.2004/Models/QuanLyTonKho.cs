using System;
using System.Collections.Generic;

namespace Server._2811._2004.Models;

public partial class QuanLyTonKho
{
    public int MaTonKho { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuongTon { get; set; }

    public DateOnly? NgayCapNhat { get; set; }

    public int? So_Luong_Da_Ban { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
