using Server._2811._2004.model1;

namespace DuAn2811_.Models
{
    public class ComboView
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int maHangSX {  get; set; }
        public List<SanPhamViewModel> AllSanPham { get; set; } = new List<SanPhamViewModel>();
        public List<SanPhamViewModel> SaleSanPham { get; set; } = new List<SanPhamViewModel>();
        public List<SanPhamViewModel> DienThoai { get; set; } = new List<SanPhamViewModel>();
        public List<SanPhamViewModel> LapTop { get; set; } = new List<SanPhamViewModel>();
        public List<SanPhamViewModel> PhuKien { get; set; } = new List<SanPhamViewModel>();
        public List<SanPhamViewModel> DongHo { get; set; } = new List<SanPhamViewModel>();
        public List<SanPhamViewModel> Smartwatch { get; set; } = new List<SanPhamViewModel>();
        public List<SanPhamViewModel> Khac { get; set; } = new List<SanPhamViewModel>();
        public List<SanPhamViewModel> SamSung { get; set; } = new List<SanPhamViewModel>();
        public List<SanPhamViewModel> RecentlyViewed { get; set; }
        public SanPhamViewModel SanPhamChiTiet { get; set; }
     
        public KhuyenMai KhuyenMai { get; set; }

        public virtual ICollection<HangSanXuat> HangSanXuats { get; set; } = new List<HangSanXuat>();

    }
}
