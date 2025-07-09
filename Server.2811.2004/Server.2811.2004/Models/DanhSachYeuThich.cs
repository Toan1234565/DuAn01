using System;
using System.Collections.Generic;

namespace Server._2811._2004.Models;

public partial class DanhSachYeuThich
{
    public int MaYeuThich { get; set; }

    public int? MaTk { get; set; }

    public int? SoLuong { get; set; }

    public virtual TaiKHoan? MaTkNavigation { get; set; }
}
