using DuAn2811_.Models;
using Newtonsoft.Json;

namespace Server._2811._2004.model1
{
    public class ChiTietSanPhamViewModel
    {
        public int? MaSanPham { get; set; }
        public int MaChiTiet { get; set; }
        public decimal? Gia { get; set; }
        public int? Soluong { get; set; }
        public string? LoaiSanPham { get; set; }
        public string? ThuocTinh { get; set; }
        public string? Mau { get; set; }
        public string? Anh { get; set; }
        public string? Dung_Luong { get; set; }
        public List<string> DanhSachAnh =>
        string.IsNullOrEmpty(Anh) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(Anh);

       
    }
}
