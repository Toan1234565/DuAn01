using System;
using System.Collections.Generic;

namespace Server._2811._2004.Models;

public partial class XuatKho
{
    public int MaXuatKho { get; set; }

    public string? DonViXuatKho { get; set; }

    public DateOnly? NgayXuat { get; set; }

    public string? GhiChi { get; set; }

    public virtual ICollection<ChiTietXuatKho> ChiTietXuatKhos { get; set; } = new List<ChiTietXuatKho>();
}
