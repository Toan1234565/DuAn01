using DuAn2811_.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Server._2811._2004.model1
{
    public class SanPhamViewModel
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }
        public int? SoLuong { get; set; }
        public decimal? Gia { get; set; }
        public string? LoaiSanPham { get; set; }
        public string? ThuocTinh { get; set; }
        public string? Mau { get; set; }
        public string? Dung_Luong { get; set; }
        public string Image { get; set; } // Vẫn giữ thuộc tính này để chứa chuỗi JSON đầy đủ (nếu cần)
       
        public SanPham SanPham { get; set; }
        public List<string> Anh { get; set; } = new List<string>();
        public string FirstImageUrl => Anh.Count > 0 ? Anh[0] : null;
        public static List<string> ParseImages(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<string>>(jsonString) ?? new List<string>();
            }
            catch (JsonException)
            {
                return new List<string>(); // Nếu lỗi, trả về danh sách rỗng
            }
        }
        public static string GetFirstImage(string jsonString)
        {
            var images = ParseImages(jsonString);
            return images.Count > 0 ? images[0] : null;
        }
        public KhuyenMai KhuyenMai { get; set; }
        public IEnumerable<ChiTietSanPhamViewModel> ChiTietSanPhams { get; set; }
        public IEnumerable<QuanLyTonKhoViewModel> QuanLyTonKhos { get; set; }
        public IEnumerable<ChiTietKhuyenMaiViewModel> ChiTietKhuyenMais { get; set; }
        public virtual HangSanXuat? MaHangSanXuatNavigation { get; set; }
    }
}