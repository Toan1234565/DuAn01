using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DonHang.Models;

public partial class TmdtContext : DbContext
{
    public TmdtContext()
    {
    }

    public TmdtContext(DbContextOptions<TmdtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaoCao> BaoCaos { get; set; }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }

    public virtual DbSet<ChiTietSanPham> ChiTietSanPhams { get; set; }

    public virtual DbSet<ChiTietXuatKho> ChiTietXuatKhos { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DanhSachYeuThich> DanhSachYeuThiches { get; set; }

    public virtual DbSet<DiaChiGiaoHang> DiaChiGiaoHangs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<HangSanXuat> HangSanXuats { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<NhapKho> NhapKhos { get; set; }

    public virtual DbSet<NhapKhoChiTiet> NhapKhoChiTiets { get; set; }

    public virtual DbSet<PhuongThucGd> PhuongThucGds { get; set; }

    public virtual DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }

    public virtual DbSet<QuanLyTonKho> QuanLyTonKhos { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<SanPhamYeuThich> SanPhamYeuThiches { get; set; }

    public virtual DbSet<TaiKHoan> TaiKHoans { get; set; }

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    public virtual DbSet<XuatKho> XuatKhos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=TOAN;Initial Catalog=TMDT;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaoCao>(entity =>
        {
            entity.HasKey(e => e.MaBaoCao).HasName("PK__BAO_CAO__3334D17924109FD7");

            entity.ToTable("BAO_CAO");

            entity.Property(e => e.MaBaoCao).HasColumnName("MA_BAO_CAO");
            entity.Property(e => e.Ghichu)
                .HasColumnType("text")
                .HasColumnName("GHICHU");
            entity.Property(e => e.NgayXuatBaoCao).HasColumnName("NGAY_XUAT_BAO_CAO");
            entity.Property(e => e.SoLuongSanPham).HasColumnName("SO_LUONG_SAN_PHAM");
            entity.Property(e => e.TenBaoCao)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TEN_BAO_CAO");
            entity.Property(e => e.TongDanhThu)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("TONG_DANH_THU");
        });

        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTietDonHang).HasName("PK__CHI_TIET__8280DDB83529F1EA");

            entity.ToTable("CHI_TIET_DON_HANG");

            entity.Property(e => e.MaChiTietDonHang).HasColumnName("MA_CHI_TIET_DON_HANG");
            entity.Property(e => e.MaDonHang).HasColumnName("MA_DON_HANG");
            entity.Property(e => e.MaSanPham).HasColumnName("MA_SAN_PHAM");
            entity.Property(e => e.SoLuongMua).HasColumnName("SO_LUONG_MUA");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK__CHI_TIET___MA_DO__5AEE82B9");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__CHI_TIET___MA_SA__5BE2A6F2");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTietGioHang).HasName("PK__CHI_TIET__D8C4F0E7C65CE1B9");

            entity.ToTable("CHI_TIET_GIO_HANG");

            entity.Property(e => e.MaChiTietGioHang).HasColumnName("MA_CHI_TIET_GIO_HANG");
            entity.Property(e => e.MaGioHang).HasColumnName("MA_GIO_HANG");
            entity.Property(e => e.MaSanPham).HasColumnName("MA_SAN_PHAM");

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaGioHang)
                .HasConstraintName("FK__CHI_TIET___MA_GI__4D94879B");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__CHI_TIET___MA_SA__4CA06362");
        });

        modelBuilder.Entity<ChiTietKhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaChiTietKhuyenMai).HasName("PK__CHI_TIET__D6DAF54FB8A48CCA");

            entity.ToTable("CHI_TIET_KHUYEN_MAI");

            entity.Property(e => e.MaChiTietKhuyenMai).HasColumnName("MA_CHI_TIET_KHUYEN_MAI");
            entity.Property(e => e.MaKhuyenMai).HasColumnName("MA_KHUYEN_MAI");
            entity.Property(e => e.MaSanPham).HasColumnName("MA_SAN_PHAM");

            entity.HasOne(d => d.MaKhuyenMaiNavigation).WithMany(p => p.ChiTietKhuyenMais)
                .HasForeignKey(d => d.MaKhuyenMai)
                .HasConstraintName("FK__CHI_TIET___MA_KH__6383C8BA");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietKhuyenMais)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__CHI_TIET___MA_SA__6477ECF3");
        });

        modelBuilder.Entity<ChiTietSanPham>(entity =>
        {
            entity.HasKey(e => e.MaChiTiet).HasName("PK__CHI_TIET__A5990A22E9B2E39A");

            entity.ToTable("CHI_TIET_SAN_PHAM");

            entity.Property(e => e.MaChiTiet).HasColumnName("MA_CHI_TIET");
            entity.Property(e => e.Gia)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("GIA");
            entity.Property(e => e.LoaiSanPham)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOAI_SAN_PHAM");
            entity.Property(e => e.MaSanPham).HasColumnName("MA_SAN_PHAM");
            entity.Property(e => e.SoLuong).HasColumnName("SO_LUONG");
            entity.Property(e => e.ThuocTinh)
                .HasColumnType("text")
                .HasColumnName("THUOC_TINH");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietSanPhams)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__CHI_TIET___MA_SA__412EB0B6");
        });

        modelBuilder.Entity<ChiTietXuatKho>(entity =>
        {
            entity.HasKey(e => e.MaChiTietXuatKho).HasName("PK__CHI_TIET__E1A792A545E53495");

            entity.ToTable("CHI_TIET_XUAT_KHO");

            entity.Property(e => e.MaChiTietXuatKho).HasColumnName("MA_CHI_TIET_XUAT_KHO");
            entity.Property(e => e.MaSanPham).HasColumnName("MA_SAN_PHAM");
            entity.Property(e => e.MaXuatKho).HasColumnName("MA_XUAT_KHO");
            entity.Property(e => e.SoLuongXuat).HasColumnName("SO_LUONG_XUAT");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietXuatKhos)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__CHI_TIET___MA_SA__72C60C4A");

            entity.HasOne(d => d.MaXuatKhoNavigation).WithMany(p => p.ChiTietXuatKhos)
                .HasForeignKey(d => d.MaXuatKho)
                .HasConstraintName("FK__CHI_TIET___MA_XU__71D1E811");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DANH_MUC__ACC6D9BBD578AF77");

            entity.ToTable("DANH_MUC");

            entity.Property(e => e.MaDanhMuc).HasColumnName("Ma_Danh_Muc");
            entity.Property(e => e.TenDanhMuc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Ten_Danh_Muc");
        });

        modelBuilder.Entity<DanhSachYeuThich>(entity =>
        {
            entity.HasKey(e => e.MaYeuThich).HasName("PK__DANH_SAC__99D274B429660D00");

            entity.ToTable("DANH_SACH_YEU_THICH");

            entity.Property(e => e.MaYeuThich).HasColumnName("MA_YEU_THICH");
            entity.Property(e => e.MaTk).HasColumnName("Ma_TK");
            entity.Property(e => e.SoLuong).HasColumnName("SO_LUONG");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.DanhSachYeuThiches)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK__DANH_SACH__Ma_TK__440B1D61");
        });

        modelBuilder.Entity<DiaChiGiaoHang>(entity =>
        {
            entity.HasKey(e => e.MaDiaChi).HasName("PK__DIA_CHI___3BCAD161FAF1A0D9");

            entity.ToTable("DIA_CHI_GIAO_HANG");

            entity.Property(e => e.MaDiaChi).HasColumnName("MA_DIA_CHI");
            entity.Property(e => e.ChiTiet)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("CHI_TIET");
            entity.Property(e => e.HoTen)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("HO_TEN");
            entity.Property(e => e.Huyen)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("HUYEN");
            entity.Property(e => e.MaTk).HasColumnName("Ma_TK");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SO_DIEN_THOAI");
            entity.Property(e => e.Tinh)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("TINH");
            entity.Property(e => e.Xa)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("XA");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.DiaChiGiaoHangs)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK__DIA_CHI_G__Ma_TK__5441852A");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang).HasName("PK__DON_HANG__A7174667195153FF");

            entity.ToTable("DON_HANG");

            entity.Property(e => e.MaDonHang).HasColumnName("MA_DON_HANG");
            entity.Property(e => e.Ghichu)
                .HasColumnType("text")
                .HasColumnName("GHICHU");
            entity.Property(e => e.MaPhuongThuc).HasColumnName("MA_PHUONG_THUC");
            entity.Property(e => e.MaPhuongThucGh).HasColumnName("MA_PHUONG_THUC_GH");
            entity.Property(e => e.MaTheoDoi)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MA_THEO_DOI");
            entity.Property(e => e.NgayDatHang).HasColumnName("NGAY_DAT_HANG");
            entity.Property(e => e.TongDonHang)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("TONG_DON_HANG");
            entity.Property(e => e.TrangThaiDonHang)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("TRANG_THAI_DON_HANG");

            entity.HasOne(d => d.MaPhuongThucNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaPhuongThuc)
                .HasConstraintName("FK__DON_HANG__MA_PHU__571DF1D5");

            entity.HasOne(d => d.MaPhuongThucGhNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaPhuongThucGh)
                .HasConstraintName("FK__DON_HANG__MA_PHU__5812160E");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang).HasName("PK__GIO_HANG__16B948ABDA4AF4F4");

            entity.ToTable("GIO_HANG");

            entity.Property(e => e.MaGioHang).HasColumnName("MA_GIO_HANG");
            entity.Property(e => e.MaTk).HasColumnName("Ma_TK");
            entity.Property(e => e.SoLuongSanPham).HasColumnName("SO_LUONG_SAN_PHAM");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK__GIO_HANG__Ma_TK__49C3F6B7");
        });

        modelBuilder.Entity<HangSanXuat>(entity =>
        {
            entity.HasKey(e => e.MaHangSanXuaT).HasName("PK__HANG_SAN__891CE63473A60D48");

            entity.ToTable("HANG_SAN_XUAT");

            entity.Property(e => e.MaHangSanXuaT).HasColumnName("Ma_Hang_San_XuaT");
            entity.Property(e => e.MaDanhMuc).HasColumnName("Ma_Danh_Muc");
            entity.Property(e => e.TenHang)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Ten_Hang");

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.HangSanXuats)
                .HasForeignKey(d => d.MaDanhMuc)
                .HasConstraintName("FK__HANG_SAN___Ma_Da__3B75D760");
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKhuyenMai).HasName("PK__KHUYEN_M__91682CA1EF119632");

            entity.ToTable("KHUYEN_MAI");

            entity.Property(e => e.MaKhuyenMai).HasColumnName("MA_KHUYEN_MAI");
            entity.Property(e => e.DieuKien)
                .HasColumnType("text")
                .HasColumnName("DIEU_KIEN");
            entity.Property(e => e.MoTaKhuyenMai)
                .HasColumnType("text")
                .HasColumnName("MO_TA_KHUYEN_MAI");
            entity.Property(e => e.NgayApDung).HasColumnName("NGAY_AP_DUNG");
            entity.Property(e => e.NgayKetThuc).HasColumnName("NGAY_KET_THUC");
            entity.Property(e => e.SoLuongKhuyenMai).HasColumnName("SO_LUONG_KHUYEN_MAI");
            entity.Property(e => e.TenKhuyemMai)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TEN_KHUYEM_MAI");
        });

        modelBuilder.Entity<NhapKho>(entity =>
        {
            entity.HasKey(e => e.MaNhapKho).HasName("PK__NHAP_KHO__6874D7D8EFFCA2CD");

            entity.ToTable("NHAP_KHO");

            entity.Property(e => e.MaNhapKho).HasColumnName("MA_NHAP_KHO");
            entity.Property(e => e.DonViNhapHang)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DON_VI_NHAP_HANG");
            entity.Property(e => e.GhiChi)
                .HasColumnType("text")
                .HasColumnName("GHI_CHI");
            entity.Property(e => e.NgayNhap).HasColumnName("NGAY_NHAP");
        });

        modelBuilder.Entity<NhapKhoChiTiet>(entity =>
        {
            entity.HasKey(e => e.NhapChiTiet).HasName("PK__NHAP_KHO__AE1443EDAABECB50");

            entity.ToTable("NHAP_KHO_CHI_TIET");

            entity.Property(e => e.NhapChiTiet).HasColumnName("NHAP_CHI_TIET");
            entity.Property(e => e.MaNhapKho).HasColumnName("MA_NHAP_KHO");
            entity.Property(e => e.MaSanPham).HasColumnName("MA_SAN_PHAM");
            entity.Property(e => e.SoLuong).HasColumnName("SO_LUONG");

            entity.HasOne(d => d.MaNhapKhoNavigation).WithMany(p => p.NhapKhoChiTiets)
                .HasForeignKey(d => d.MaNhapKho)
                .HasConstraintName("FK__NHAP_KHO___MA_NH__6C190EBB");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.NhapKhoChiTiets)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__NHAP_KHO___MA_SA__6D0D32F4");
        });

        modelBuilder.Entity<PhuongThucGd>(entity =>
        {
            entity.HasKey(e => e.MaPhuongThucGd).HasName("PK__PHUONG_T__DD4C335AF3D5BDEF");

            entity.ToTable("PHUONG_THUC_GD");

            entity.Property(e => e.MaPhuongThucGd).HasColumnName("MA_PHUONG_THUC_GD");
            entity.Property(e => e.TenPhuongThucGd)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("TEN_PHUONG_THUC_GD");
        });

        modelBuilder.Entity<PhuongThucThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaPhuongThuc).HasName("PK__PHUONG_T__31662E8181F1F5F9");

            entity.ToTable("PHUONG_THUC_THANH_TOAN");

            entity.Property(e => e.MaPhuongThuc).HasColumnName("MA_PHUONG_THUC");
            entity.Property(e => e.TenPhuongThuc)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("TEN_PHUONG_THUC");
        });

        modelBuilder.Entity<QuanLyTonKho>(entity =>
        {
            entity.HasKey(e => e.MaTonKho).HasName("PK__QUAN_LY___5CC9326D6AC70FFF");

            entity.ToTable("QUAN_LY_TON_KHO");

            entity.Property(e => e.MaTonKho).HasColumnName("MA_TON_KHO");
            entity.Property(e => e.MaSanPham).HasColumnName("MA_SAN_PHAM");
            entity.Property(e => e.NgayCapNhat).HasColumnName("NGAY_CAP_NHAT");
            entity.Property(e => e.SoLuongDaBan).HasColumnName("So_Luong_Da_Ban");
            entity.Property(e => e.SoLuongTon).HasColumnName("SO_LUONG_TON");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.QuanLyTonKhos)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__QUAN_LY_T__MA_SA__6754599E");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham).HasName("PK__SAN_PHAM__AEAADD69E53C75FB");

            entity.ToTable("SAN_PHAM");

            entity.HasIndex(e => e.Anh, "IDX_SAN_PHAM_ANH");

            entity.HasIndex(e => e.TenSanPham, "IDX_SAN_PHAM_TEN_SAN_PHAM");

            entity.Property(e => e.MaSanPham).HasColumnName("MA_SAN_PHAM");
            entity.Property(e => e.Anh)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ANH");
            entity.Property(e => e.MaHangSanXuat).HasColumnName("Ma_Hang_San_Xuat");
            entity.Property(e => e.MoTa)
                .HasColumnType("text")
                .HasColumnName("MO_TA");
            entity.Property(e => e.TenSanPham)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TEN_SAN_PHAM");

            entity.HasOne(d => d.MaHangSanXuatNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaHangSanXuat)
                .HasConstraintName("FK__SAN_PHAM__Ma_Han__3E52440B");
        });

        modelBuilder.Entity<SanPhamYeuThich>(entity =>
        {
            entity.HasKey(e => e.MaChiTietYeuThich).HasName("PK__SAN_PHAM__CC600104A01C8BF6");

            entity.ToTable("SAN_PHAM_YEU_THICH");

            entity.Property(e => e.MaChiTietYeuThich).HasColumnName("MA_CHI_TIET_YEU_THICH");
            entity.Property(e => e.MaSanPham).HasColumnName("MA_SAN_PHAM");
            entity.Property(e => e.SoLuong).HasColumnName("SO_LUONG");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.SanPhamYeuThiches)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__SAN_PHAM___MA_SA__46E78A0C");
        });

        modelBuilder.Entity<TaiKHoan>(entity =>
        {
            entity.HasKey(e => e.MaTk).HasName("PK__TAI_kHOA__2E62FB7C7573F511");

            entity.ToTable("TAI_kHOAN");

            entity.HasIndex(e => e.MatKhau, "IDX_TAI_KHOAN_MK");

            entity.HasIndex(e => e.TenDangNhap, "IDX_TAI_KHOAN_TEN");

            entity.Property(e => e.MaTk).HasColumnName("Ma_TK");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EmailToken).HasMaxLength(255);
            entity.Property(e => e.IsEmailConfirmed).HasDefaultValue(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaThanhToan).HasName("PK__THANH_TO__D82DF910B79C5264");

            entity.ToTable("THANH_TOAN");

            entity.Property(e => e.MaThanhToan).HasColumnName("MA_THANH_TOAN");
            entity.Property(e => e.MaDonHang).HasColumnName("MA_DON_HANG");
            entity.Property(e => e.MaPhuongThuc).HasColumnName("MA_PHUONG_THUC");
            entity.Property(e => e.MaPhuongThucGh).HasColumnName("MA_PHUONG_THUC_GH");
            entity.Property(e => e.NgayThanhToan).HasColumnName("NGAY_THANH_TOAN");
            entity.Property(e => e.SoTienCanThanhToan)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("SO_TIEN_CAN_THANH_TOAN");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TRANG_THAI");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK__THANH_TOA__MA_DO__5EBF139D");
        });

        modelBuilder.Entity<XuatKho>(entity =>
        {
            entity.HasKey(e => e.MaXuatKho).HasName("PK__XUAT_KHO__36F544110388E8A4");

            entity.ToTable("XUAT_KHO");

            entity.Property(e => e.MaXuatKho).HasColumnName("MA_XUAT_KHO");
            entity.Property(e => e.DonViXuatKho)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DON_VI_XUAT_KHO");
            entity.Property(e => e.GhiChi)
                .HasColumnType("text")
                .HasColumnName("GHI_CHI");
            entity.Property(e => e.NgayXuat).HasColumnName("NGAY_XUAT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
