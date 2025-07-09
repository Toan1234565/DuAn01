using Server._2811._2004.model1;

namespace DuAn2811_.Models
{
    public class ComboView
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public List<SanPhamViewModel> AllSanPham { get; set; } = new List<SanPhamViewModel>();
        public List<SanPhamViewModel> SaleSanPham { get; set; } = new List<SanPhamViewModel>();
    }
}
