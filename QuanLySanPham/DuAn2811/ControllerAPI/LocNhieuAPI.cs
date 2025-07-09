using DuAn2811_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Server._2811._2004.model1;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Server._2811._2004.ControllerAPI.ContosoUniversity;

namespace DuAn2811_.ControllerAPI
{
    [Route("api/locsanphamAll")]
    [ApiController]
    public class LocNhieuAPI : ControllerBase
    {
        private readonly TmdtContext _db;
        private readonly IMemoryCache _cache;

        public LocNhieuAPI(TmdtContext context, IMemoryCache cache)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet("GetSanPham1")]
        public async Task<IActionResult> LocSanPham(
            int? maDanhMuc,
            int? maHangSanXuat,
            decimal? giatu,
            decimal? giaden,
            int? pageIndex,
            int pageSize = 20)
        {
            try
            {
                if (_db == null)
                {
                    return StatusCode(500, new { Message = "Database chưa được khởi tạo" });
                }

                int currentPage = pageIndex.HasValue && pageIndex > 0 ? pageIndex.Value : 1;
                string cacheKey = $"Products_DM{maDanhMuc}_HSX{maHangSanXuat}_GT{giatu}_GD{giaden}_P{currentPage}_S{pageSize}";

                // Kiểm tra cache
                if (_cache.TryGetValue(cacheKey, out ComboView? cachedData))
                {
                    return Ok(cachedData);
                }

                var sanphamQuery = _db.SanPhams
                    .Include(sp => sp.MaHangSanXuatNavigation)
                        .ThenInclude(hsx => hsx.MaDanhMucNavigation)
                    .Include(sp => sp.ChiTietSanPhams)
                    .Include(sp => sp.QuanLyTonKhos);
                    

                if (maDanhMuc.HasValue)
                {
                    sanphamQuery = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<SanPham, ICollection<QuanLyTonKho>>)sanphamQuery.Where(sp => sp.MaHangSanXuatNavigation != null &&
                                                     sp.MaHangSanXuatNavigation.MaDanhMucNavigation != null &&
                                                     sp.MaHangSanXuatNavigation.MaDanhMucNavigation.MaDanhMuc == maDanhMuc);
                }

                if (maHangSanXuat.HasValue)
                {
                    sanphamQuery = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<SanPham, ICollection<QuanLyTonKho>>)sanphamQuery.Where(sp => sp.MaHangSanXuat == maHangSanXuat);
                }

                if (giatu.HasValue)
                {
                    sanphamQuery = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<SanPham, ICollection<QuanLyTonKho>>)sanphamQuery.Where(sp => sp.ChiTietSanPhams.Any(ct => ct.Gia >= giatu));
                }

                if (giaden.HasValue)
                {
                    sanphamQuery = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<SanPham, ICollection<QuanLyTonKho>>)sanphamQuery.Where(sp => sp.ChiTietSanPhams.Any(ct => ct.Gia <= giaden));
                }

                var sanphamViewModelQuery = sanphamQuery.Select(sp => new SanPhamViewModel
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    MoTa = sp.MoTa,
                    Anh = JsonConvert.DeserializeObject<List<string>>(sp.Anh), // Chuyển đổi JSON sang List<string>
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

                var pageSanPham = await PaginatedList<SanPhamViewModel>.CreateAsync(sanphamViewModelQuery, currentPage, pageSize);

                if (!pageSanPham.Any())
                {
                    return NotFound(new { Message = "Không có sản phẩm nào phù hợp với bộ lọc!" });
                }

                var result = new ComboView
                {
                    AllSanPham = pageSanPham.ToList(),
                    CurrentPage = currentPage,
                    TotalPages = pageSanPham.TotaPages,
                    PageSize = pageSize
                };

                // Lưu thông tin vào cache
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(cacheKey, result, cacheEntryOptions);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lọc sản phẩm.", ErrorDetails = ex.Message });
            }
        }
    }
}