using DuAn2811_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory; // Thêm thư viện caching
using Server._2811._2004.ControllerAPI;
using Server._2811._2004.model1;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json; // Thêm thư viện Newtonsoft.Json

namespace DuAn2811_.ControllerAPI
{
    [Route("api/QuanLySanPhamAPI")]
    [ApiController]
    public class QuanLySanPhamAPI : ControllerBase
    {
        private readonly TmdtContext db;
        private readonly IMemoryCache _cache; // Thêm biến cache

        public QuanLySanPhamAPI(TmdtContext context, IMemoryCache memoryCache)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult> GetAllProducts(int? pageIndex, int pageSize = 20)
        {
            try
            {
                if (db == null)
                {
                    return StatusCode(500, new { Message = "Database chưa được khởi tạo." });
                }

                int currentPage = pageIndex.HasValue && pageIndex > 0 ? pageIndex.Value : 1;
                string cacheKey = $"Products_{currentPage}_{pageSize}"; // Key để lưu cache

                if (_cache.TryGetValue(cacheKey, out object cachedData))
                {
                    return Ok(cachedData); // Nếu có cache, trả về dữ liệu đã lưu
                }
                var sanphamQuery = db.SanPhams
                    .Include(sp => sp.ChiTietSanPhams)
                    .Include(sp => sp.QuanLyTonKhos)
                    .Include(sp => sp.ChiTietKhuyenMais)
                        .ThenInclude(ctkm => ctkm.MaKhuyenMaiNavigation)
                    .Select(sp => new SanPhamViewModel
                    {
                        MaSanPham = sp.MaSanPham,
                        TenSanPham = sp.TenSanPham,
                        MoTa = sp.MoTa,
                        Image = sp.Anh ?? "[]",
                        Anh = SanPhamViewModel.ParseImages(sp.Anh ?? "[]"),

                        ChiTietSanPhams = sp.ChiTietSanPhams.Select(ct => new ChiTietSanPhamViewModel
                        {
                            MaSanPham = ct.MaSanPham,
                            MaChiTiet = ct.MaChiTiet,
                            Gia = ct.Gia,
                            Mau = ct.Mau,
                            Dung_Luong = ct.Dung_Luong
                        }),
                        QuanLyTonKhos = sp.QuanLyTonKhos.Select(qt => new QuanLyTonKhoViewModel
                        {
                            MaSanPham = qt.MaSanPham,
                            SoLuongTon = qt.SoLuongTon,
                            So_Luong_Da_Ban = qt.So_Luong_Da_Ban
                        }),
                        ChiTietKhuyenMais = sp.ChiTietKhuyenMais.Select(ctkm => new ChiTietKhuyenMaiViewModel
                        {
                            MaChiTietKhuyenMai = ctkm.MaChiTietKhuyenMai,
                            TenKhuyenMai = ctkm.MaKhuyenMaiNavigation != null ? ctkm.MaKhuyenMaiNavigation.TenKhuyemMai : ""
                        })
                    });
                
                var pagedSanPham = await ContosoUniversity.PaginatedList<SanPhamViewModel>
                                                .CreateAsync(sanphamQuery, currentPage, pageSize);

                if (!pagedSanPham.Any())
                {
                    return NotFound(new { Message = "Không có sản phẩm nào để hiển thị!" });
                }
            
                var result = new ComboView
                {
                    AllSanPham = pagedSanPham?.ToList() ?? new List<SanPhamViewModel>(),
                    SaleSanPham = pagedSanPham?.Where(sp => sp.ChiTietKhuyenMais.Any()).ToList() ?? new List<SanPhamViewModel>(),
                    CurrentPage = currentPage,
                    TotalPages = pagedSanPham?.TotaPages ?? 0,
                    PageSize = pageSize
                };
                // Lưu vào cache trong 5 phút
                _cache.Set(cacheKey, result, new MemoryCacheEntryOptions
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
    }
}