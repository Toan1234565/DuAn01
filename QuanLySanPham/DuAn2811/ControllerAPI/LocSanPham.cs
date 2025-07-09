using DuAn2811_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Server._2811._2004.model1;


namespace Server._2811._2004.ControllerAPI
{
    [Route("api/SanPham")]
    [ApiController]
    public class LocSanPham : ControllerBase
    {
        private readonly TmdtContext db;
        private readonly IMemoryCache _cache;

        public LocSanPham(TmdtContext context, IMemoryCache memoryCache)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        [HttpGet("LocSanPham")]
        //public async Task<IActionResult> filterproducts(
        //    int? maDanhMuc,
        //    int? maHangSanXuat,
        //    string sortOrder = "gia_asc",
        //    int? pageIndex,
        //    int pageSize = 20)
        public async Task<IActionResult> filterproducts(int? maDanhMuc, int? maHangSanXuat, int? pageIndex, int pageSize = 20, string sortOrder = "gia_asc")
        {
            try
            {
                if (db == null)
                {
                    return StatusCode(500, new { Message = "Database chưa được khởi tạo." });
                }

                int currentPage = pageIndex.HasValue && pageIndex > 0 ? pageIndex.Value : 1;
                string cacheKey = $"Products_DM{maDanhMuc}_HSX{maHangSanXuat}_Page{currentPage}_Size{pageSize}_Sort{sortOrder}";

                if (_cache.TryGetValue(cacheKey, out ComboView cachedData))
                {
                    return Ok(cachedData);
                }

                var sanphamQuery = db.SanPhams
                    .Include(sp => sp.MaHangSanXuatNavigation)
                        .ThenInclude(hsx => hsx.MaDanhMucNavigation)
                    .Include(sp => sp.ChiTietSanPhams)
                    .Include(sp => sp.QuanLyTonKhos)
                    .Include(sp => sp.ChiTietKhuyenMais)
                        .ThenInclude(ctkm => ctkm.MaKhuyenMaiNavigation)
                    .AsQueryable();

                if (maDanhMuc.HasValue)
                {
                    sanphamQuery = sanphamQuery.Where(sp => sp.MaHangSanXuatNavigation.MaDanhMucNavigation.MaDanhMuc == maDanhMuc);
                }

                if (maHangSanXuat.HasValue)
                {
                    sanphamQuery = sanphamQuery.Where(sp => sp.MaHangSanXuatNavigation.MaHangSanXuaT == maHangSanXuat);
                }

                sanphamQuery = SortProducts(sanphamQuery, sortOrder);

                var sanPhamViewModelQuery = sanphamQuery.Select(sp => new SanPhamViewModel
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    MoTa = sp.MoTa,
                    Anh = SanPhamViewModel.ParseImages(sp.Anh ?? "[]"),
                    ChiTietSanPhams = sp.ChiTietSanPhams.Select(ct => new ChiTietSanPhamViewModel
                    {
                        MaSanPham = ct.MaSanPham,
                        MaChiTiet = ct.MaChiTiet,
                        Gia = ct.Gia,
                        Mau = ct.Mau,
                        Dung_Luong = ct.Dung_Luong
                    }).ToList(),
                    QuanLyTonKhos = sp.QuanLyTonKhos.Select(qt => new QuanLyTonKhoViewModel
                    {
                        MaSanPham = qt.MaSanPham,
                        SoLuongTon = qt.SoLuongTon
                    }).ToList(),
                    ChiTietKhuyenMais = sp.ChiTietKhuyenMais.Select(ctkm => new ChiTietKhuyenMaiViewModel
                    {
                        TenKhuyenMai = ctkm.MaKhuyenMaiNavigation != null ? ctkm.MaKhuyenMaiNavigation.TenKhuyemMai : ""
                    }).ToList()
                });

                var pagedSanPham = await ContosoUniversity.PaginatedList<SanPhamViewModel>.CreateAsync(sanPhamViewModelQuery, currentPage, pageSize);

                if (!pagedSanPham.Any())
                {
                    return NotFound(new { Message = "Không có sản phẩm nào phù hợp với tiêu chí lọc." });
                }

                var result = new ComboView
                {
                    AllSanPham = pagedSanPham.ToList(),
                    CurrentPage = currentPage,
                    TotalPages = pagedSanPham.TotaPages,
                    PageSize = pageSize
                };

                _cache.Set(cacheKey, result, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lọc sản phẩm.", ErrorDetails = ex.Message });
            }
        }

        private IQueryable<SanPham> SortProducts(IQueryable<SanPham> query, string sortOrder)
        {
            switch (sortOrder.ToLower())
            {
                case "gia_desc":
                    return query.OrderByDescending(sp => sp.ChiTietSanPhams.Min(ct => ct.Gia));
                case "gia_asc":
                default:
                    return query.OrderBy(sp => sp.ChiTietSanPhams.Min(ct => ct.Gia));
            }
        }
    }
}