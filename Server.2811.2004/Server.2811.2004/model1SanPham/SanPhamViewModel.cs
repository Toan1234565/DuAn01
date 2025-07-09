namespace Server._2811._2004.model1
{
    public class SanPhamViewModel
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }
        public string Anh { get; set; }
        public IEnumerable<ChiTietSanPhamViewModel> ChiTietSanPhams { get; set; }
        public IEnumerable<QuanLyTonKhoViewModel> QuanLyTonKhos { get; set; }
        public IEnumerable<ChiTietKhuyenMaiViewModel> ChiTietKhuyenMais { get; set; }
    }
}
