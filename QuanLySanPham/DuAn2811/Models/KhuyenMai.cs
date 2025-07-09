using System;
using System.Collections.Generic;

namespace DuAn2811_.Models;

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
