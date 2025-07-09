using DuAn2811_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Win32.SafeHandles;
using Server._2811._2004.model1;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Server._2811._2004.ControllerAPI
{
    [Route("api/chitiet")]
    [ApiController]
    public class ChiTietSanPham : ControllerBase
    {
        private readonly TmdtContext db;
        private readonly IMemoryCache _cache;
        private readonly string _recentlyViewedKey = "RecentlyViewedProducts"; // Khóa cho Session
        private readonly ILogger<ChiTietSanPham> _logger;

        public ChiTietSanPham(TmdtContext context, IMemoryCache cache, ILogger<ChiTietSanPham> logger)
        {
            db = context ?? throw new ArgumentNullException(nameof(db));
            _cache = cache ?? throw new ArgumentNullException(nameof(_cache));
            _logger = logger ??throw new ArgumentException(nameof(logger)) ;
        }
        // Định nghĩa một lớp để đại diện cho cấu trúc JSON của ảnh (nếu cần)
        public class ImageUrl
        {
            public string Url { get; set; }
            // Có thể có thêm các thuộc tính khác nếu cấu trúc JSON phức tạp hơn
        }
        // API lấy thông tin chi tiết sản phẩm khi nhấp vào ảnh hoặc tên sản phẩm
        [HttpGet("GetChiTietsanpham")]
        public async Task<IActionResult> ChitietSanPham(int id)
        {
            try
            {
                if (db == null)
                {
                    return StatusCode(500, new { Message = "database chua duoc khoi tao" });
                }

                string cachekey = $"Products_Ct{id}";

                //kiem tra cache

                if(_cache.TryGetValue(cachekey, out var cachedSanPham))
                {
                    // Lưu sản phẩm đã xem vào Session
                    LuuSanPhamDaXemVaoSession(id, cachedSanPham as SanPhamViewModel);
                    return Ok(cachedSanPham);
                }
                var sanpham = await db.SanPhams
                    .Include(sp => sp.QuanLyTonKhos)
                    .Include(sp => sp.ChiTietSanPhams)
                    .Include(sp => sp.ChiTietKhuyenMais)
                    .ThenInclude(ctkm => ctkm.MaKhuyenMaiNavigation)
                    .Where(sp => sp.MaSanPham == id)
                    .FirstOrDefaultAsync();
                if (sanpham == null)
                {
                    return NotFound(new { Message = $"Không tìm thấy sản phẩm với ID: {id}" });
                }
                List<string> danhsachAnh = new List<string>();
                if (!string.IsNullOrEmpty(sanpham.Anh))
                {
                    try
                    {
                        // thu deserialize truc tiep thanh list<string> neu json chi la mang cac duong dan \
                        danhsachAnh = JsonSerializer.Deserialize<List<string>>(sanpham.Anh);
                        if(danhsachAnh == null)
                        {
                            // neu khong phai mang string, thu deserialize thanh mang cac doi tuong co thuoc tinh url
                            var imageObjects = JsonSerializer.Deserialize<List<ImageUrl>>(sanpham.Anh);
                            if(imageObjects != null)
                            {
                                danhsachAnh = imageObjects.Select(img => img.Url).ToList();
                            }
                        }
                    }
                    catch (JsonException ex)
                    {
                        // Xử lý lỗi nếu chuỗi JSON không hợp lệ
                        return StatusCode(500, new { Message = "Lỗi khi giải mã JSON của ảnh.", Details = ex.Message });
                    }
                }             
                var result = new SanPhamViewModel
                {
                    MaSanPham = sanpham.MaSanPham,
                    TenSanPham = sanpham.TenSanPham,
                    MoTa = sanpham.MoTa,
                    Anh = danhsachAnh,
                    ChiTietSanPhams = sanpham.ChiTietSanPhams.Select(ct => new ChiTietSanPhamViewModel
                    {
                        MaSanPham = ct.MaSanPham,
                        MaChiTiet = ct.MaChiTiet,
                        Gia = ct.Gia,
                        Soluong = ct.SoLuong,
                        LoaiSanPham = ct.LoaiSanPham,
                        ThuocTinh = ct.ThuocTinh,
                        Mau = ct.Mau

                    }).ToList(),
                    QuanLyTonKhos = sanpham.QuanLyTonKhos.Select(qt => new QuanLyTonKhoViewModel
                    {
                        MaSanPham = qt.MaSanPham,
                        SoLuongTon = qt.SoLuongTon,
                        So_Luong_Da_Ban = qt.So_Luong_Da_Ban
                    }).ToList(),
                    ChiTietKhuyenMais = sanpham.ChiTietKhuyenMais.Select(ctkm => new ChiTietKhuyenMaiViewModel
                    {
                        MaChiTietKhuyenMai = ctkm.MaChiTietKhuyenMai,
                        TenKhuyenMai = ctkm.MaKhuyenMaiNavigation?.TenKhuyemMai ?? "",
                        MoTaKhuyenMai = ctkm.MaKhuyenMaiNavigation?.MoTaKhuyenMai ?? ""
                    }).ToList()
                };

                // Lưu vào cache trong 5 phút
                _cache.Set(cachekey, result, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2) // Làm mới nếu có truy cập
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Đã xảy ra lỗi không xác định.", Details = ex.Message });
            }
        }
        [HttpGet("GetSanPhamDaXem")]
        public IActionResult GetSanPhamDaXem()
        {
            // Lấy ComboView từ Session
            var recentlyViewedJson = HttpContext.Session.GetString(_recentlyViewedKey);

            if (string.IsNullOrEmpty(recentlyViewedJson))
            {
                return Ok(new List<SanPhamViewModel>()); // Trả về danh sách rỗng nếu không có dữ liệu
            }

            try
            {
                var recentlyViewedCombo = JsonSerializer.Deserialize<ComboView>(recentlyViewedJson);
                if (recentlyViewedCombo?.RecentlyViewed != null)
                {
                    return Ok(recentlyViewedCombo.RecentlyViewed);
                }
                else
                {
                    return Ok(new List<SanPhamViewModel>()); // Trả về danh sách rỗng nếu RecentlyViewed là null
                }
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Lỗi khi deserialize ComboView từ Session (đọc): {ex.Message}");
                return StatusCode(500, new { Message = "Lỗi khi đọc dữ liệu sản phẩm đã xem." });
            }
        }

        private void LuuSanPhamDaXemVaoSession(int productId, SanPhamViewModel product)
        {
            // Lấy ComboView từ Session (nếu có)
            var recentlyViewedJson = HttpContext.Session.GetString(_recentlyViewedKey);
            ComboView recentlyViewedCombo = null;

            if (!string.IsNullOrEmpty(recentlyViewedJson))
            {
                try
                {
                    recentlyViewedCombo = JsonSerializer.Deserialize<ComboView>(recentlyViewedJson);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"Lỗi khi deserialize ComboView từ Session (lưu): {ex.Message}");
                    // Xử lý lỗi, có thể tạo mới ComboView
                }
            }

            // Nếu chưa có ComboView trong Session, tạo mới
            if (recentlyViewedCombo == null)
            {
                recentlyViewedCombo = new ComboView { RecentlyViewed = new List<SanPhamViewModel>() };
            }

            // Lấy danh sách RecentlyViewed từ ComboView
            var recentlyViewedList = recentlyViewedCombo.RecentlyViewed;

            // Kiểm tra xem sản phẩm đã tồn tại trong danh sách chưa
            if (!recentlyViewedList.Any(p => p.MaSanPham == productId))
            {
                // Thêm sản phẩm mới vào đầu danh sách
                recentlyViewedList.Insert(0, product);

                // Giới hạn số lượng sản phẩm đã xem
                if (recentlyViewedList.Count > 5)
                {
                    recentlyViewedList.RemoveAt(recentlyViewedList.Count - 1);
                }

                // Cập nhật lại thuộc tính RecentlyViewed của ComboView
                recentlyViewedCombo.RecentlyViewed = recentlyViewedList;

                // Cập nhật lại Session với ComboView đã được serialize
                try
                {
                    HttpContext.Session.SetString(_recentlyViewedKey, JsonSerializer.Serialize(recentlyViewedCombo));
                    _logger.LogInformation($"Đã lưu sản phẩm {productId} vào RecentlyViewed của ComboView trong Session.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Lỗi khi serialize và lưu ComboView vào Session: {ex.Message}");
                }
            }
            else
            {
                // Nếu sản phẩm đã tồn tại, di chuyển nó lên đầu danh sách
                var existingProduct = recentlyViewedList.FirstOrDefault(p => p.MaSanPham == productId);
                if (existingProduct != null)
                {
                    recentlyViewedList.Remove(existingProduct);
                    recentlyViewedList.Insert(0, existingProduct);

                    // Cập nhật lại thuộc tính RecentlyViewed của ComboView
                    recentlyViewedCombo.RecentlyViewed = recentlyViewedList;

                    // Cập nhật lại Session với ComboView đã được serialize
                    try
                    {
                        HttpContext.Session.SetString(_recentlyViewedKey, JsonSerializer.Serialize(recentlyViewedCombo));
                        _logger.LogInformation($"Đã cập nhật thứ tự xem cho sản phẩm {productId} trong RecentlyViewed của ComboView trong Session.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Lỗi khi serialize và cập nhật ComboView vào Session: {ex.Message}");
                    }
                }
            }

        }


    }
}

