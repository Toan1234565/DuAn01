using System;
using System.Collections.Generic;

namespace TaiKhoan.Models;

public partial class DiaChiGiaoHang
{
    public int MaDiaChi { get; set; }

    public int? MaTk { get; set; }

    public string? HoTen { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Tinh { get; set; }

    public string? Huyen { get; set; }

    public string? Xa { get; set; }

    public string? ChiTiet { get; set; }

    public virtual TaiKHoan? MaTkNavigation { get; set; }
}
