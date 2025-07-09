using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nest;
using NuGet.Protocol.Plugins;
using Server._2811._2004.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server._2811._2004.ControllerAPI
{
    [Route("api/TimKiemSanPham")]
    [ApiController]
    public class TimKiemSanPham : ControllerBase
    {
        private readonly ElasticSearchService _elasticSearchService;
        private readonly ILogger<TimKiemSanPham> _logger;
        private readonly IMemoryCache _cache; // Thêm biến cache

        public TimKiemSanPham(ElasticSearchService elasticSearchService, ILogger<TimKiemSanPham> logger, IMemoryCache memoryCache)
        {
            _elasticSearchService = elasticSearchService;
            _logger = logger;
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            //kiểm tra xem người dùng có nhập thông tin vào ô tìm kiếm hay không.
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { Message = "Từ khóa tìm kiếm không được để trống." });
            }
            //kiểm tra cache nếu đã có trong cache thì nếu dữ liệu trong cache không cần truy cập CSDL 
            string cachekey = $"Search_{query}";
            if (_cache.TryGetValue(cachekey, out object cachedData))
            {
                return Ok(cachedData); // Nếu có cache, trả về dữ liệu đã lưu
            }

            try
            {
                
                // gọi dịch vụ tìm kiếm sự dụng công cụ timf kiếm ...
                var productIds = await _elasticSearchService.SearchProductsAsync(query);

                // kiểm tra kết quả tìm được 
                if (productIds == null || productIds.Count == 0)
                {
                    return NotFound(new { Message = "Không tìm thấy sản phẩm nào phù hợp.", SearchQuery = query });
                }
                // luu thong tin tim kiem vao cache trong 10p
               
                _cache.Set(cachekey, productIds, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(2) // Làm mới nếu có truy cập
                });

                // trả dư liệu
                return Ok(new { Message = "Danh sách mã sản phẩm tìm được:", ProductIds = productIds });
            }
            // thực hiện bắt lỗi 
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Lỗi trong truy vấn cơ sở dữ liệu");
                return StatusCode(500, new { Message = "Lỗi trong truy vấn cơ sở dữ liệu.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi không xác định.");
                return StatusCode(500, new { Message = "Đã xảy ra lỗi không xác định.", Details = ex.Message });
            }
        }
    }
}
