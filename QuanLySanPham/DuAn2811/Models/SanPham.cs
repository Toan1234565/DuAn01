using System;
using System.Collections.Generic;

namespace DuAn2811_.Models;

public partial class SanPham
{
    public int MaSanPham { get; set; }

    public string? TenSanPham { get; set; }

    public string? MoTa { get; set; }

    public int? MaHangSanXuat { get; set; }

    public string? Anh { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; } = new List<ChiTietKhuyenMai>();

    public virtual ICollection<ChiTietSanPham> ChiTietSanPhams { get; set; } = new List<ChiTietSanPham>();

    public virtual ICollection<ChiTietXuatKho> ChiTietXuatKhos { get; set; } = new List<ChiTietXuatKho>();

    public virtual HangSanXuat? MaHangSanXuatNavigation { get; set; }

    public virtual ICollection<NhapKhoChiTiet> NhapKhoChiTiets { get; set; } = new List<NhapKhoChiTiet>();

    public virtual ICollection<QuanLyTonKho> QuanLyTonKhos { get; set; } = new List<QuanLyTonKho>();

    public virtual ICollection<SanPhamYeuThich> SanPhamYeuThiches { get; set; } = new List<SanPhamYeuThich>();
}
