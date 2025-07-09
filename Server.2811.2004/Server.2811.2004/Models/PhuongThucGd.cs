using System;
using System.Collections.Generic;

namespace Server._2811._2004.Models;

public partial class PhuongThucGd
{
    public int MaPhuongThucGd { get; set; }

    public string? TenPhuongThucGd { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
