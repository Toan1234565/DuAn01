using System;
using System.Collections.Generic;

namespace ThanhToan.Models;

public partial class NhapKho
{
    public int MaNhapKho { get; set; }

    public string? DonViNhapHang { get; set; }

    public DateOnly? NgayNhap { get; set; }

    public string? GhiChi { get; set; }

    public virtual ICollection<NhapKhoChiTiet> NhapKhoChiTiets { get; set; } = new List<NhapKhoChiTiet>();
}
