using System;
using System.Collections.Generic;

namespace DuAn2811_.Models;

public partial class HangSanXuat
{
    public int MaHangSanXuaT { get; set; }

    public int? MaDanhMuc { get; set; }

    public string? TenHang { get; set; }

    public virtual DanhMuc? MaDanhMucNavigation { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
