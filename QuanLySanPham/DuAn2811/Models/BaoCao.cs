using System;
using System.Collections.Generic;

namespace DuAn2811_.Models;

public partial class BaoCao
{
    public int MaBaoCao { get; set; }

    public string? TenBaoCao { get; set; }

    public int? SoLuongSanPham { get; set; }

    public decimal? TongDanhThu { get; set; }

    public DateOnly? NgayXuatBaoCao { get; set; }

    public string? Ghichu { get; set; }
}
