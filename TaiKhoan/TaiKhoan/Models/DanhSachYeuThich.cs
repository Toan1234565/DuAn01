using System;
using System.Collections.Generic;

namespace TaiKhoan.Models;

public partial class DanhSachYeuThich
{
    public int MaYeuThich { get; set; }

    public int? MaTk { get; set; }

    public int? SoLuong { get; set; }

    public virtual TaiKHoan? MaTkNavigation { get; set; }
}
