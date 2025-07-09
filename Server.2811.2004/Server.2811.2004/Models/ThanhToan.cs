using System;
using System.Collections.Generic;

namespace Server._2811._2004.Models;

public partial class ThanhToan
{
    public int MaThanhToan { get; set; }

    public int? MaDonHang { get; set; }

    public DateOnly? NgayThanhToan { get; set; }

    public int? MaPhuongThuc { get; set; }

    public int? MaPhuongThucGh { get; set; }

    public decimal? SoTienCanThanhToan { get; set; }

    public string? TrangThai { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }
}
