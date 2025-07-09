using System;
using System.Collections.Generic;

namespace DonHang.Models;

public partial class DanhMuc
{
    public int MaDanhMuc { get; set; }

    public string? TenDanhMuc { get; set; }

    public virtual ICollection<HangSanXuat> HangSanXuats { get; set; } = new List<HangSanXuat>();
}
