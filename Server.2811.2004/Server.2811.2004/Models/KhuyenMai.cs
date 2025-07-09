using System;
using System.Collections.Generic;

namespace Server._2811._2004.Models;

public partial class KhuyenMai
{
    public int MaKhuyenMai { get; set; }

    public string? TenKhuyemMai { get; set; }

    public string? MoTaKhuyenMai { get; set; }

    public DateOnly? NgayApDung { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    public int? SoLuongKhuyenMai { get; set; }

    public string? DieuKien { get; set; }

    public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; } = new List<ChiTietKhuyenMai>();
}
