using DuAn2811_.Models;

namespace Server._2811._2004.model1
{
    public class HangSanXuat
    {
        public int MaHangSanXuaT { get; set; }

        public int? MaDanhMuc { get; set; }

        public string? TenHang { get; set; }

        public virtual DanhMuc? MaDanhMucNavigation { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
    }
}
