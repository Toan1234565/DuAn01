using System;
using System.Collections.Generic;

namespace GioHang.com.Models;

public partial class DonHang
{
    public int MaDonHang { get; set; }

    public DateOnly? NgayDatHang { get; set; }

    public string? TrangThaiDonHang { get; set; }

    public decimal? TongDonHang { get; set; }

    public int? MaPhuongThuc { get; set; }

    public int? MaPhuongThucGh { get; set; }

    public string? MaTheoDoi { get; set; }

    public string? Ghichu { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual PhuongThucGd? MaPhuongThucGhNavigation { get; set; }

    public virtual PhuongThucThanhToan? MaPhuongThucNavigation { get; set; }

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
