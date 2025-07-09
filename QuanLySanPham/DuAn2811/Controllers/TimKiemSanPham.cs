using DuAn2811_.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Server._2811._2004.Controllers
{
    public class TimKiemSanPham : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7247/api/TimKiemSanPham";

        public TimKiemSanPham(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TimKiem(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { Message = "Từ khóa tìm kiếm không được để trống." });
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/search?query={query}")
                                                .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode,
                        new { Message = "Lỗi API: Không thể lấy dữ liệu.", Status = response.StatusCode });
                }

                var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                // Kiểm tra nếu phản hồi rỗng hoặc lỗi
                if (string.IsNullOrWhiteSpace(responseData))
                {
                    return NotFound(new { Message = "Không có sản phẩm nào được tìm thấy." });
                }

                var products = JsonConvert.DeserializeObject<List<ComboView>>(responseData);


                if (products == null || products.Count == 0)
                {
                    return StatusCode(500, new { Message = "Lỗi khi chuyển đổi dữ liệu từ API." });
                }

                return View(products);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { Message = "Lỗi kết nối đến API.", Details = ex.Message });
            }
            catch (JsonSerializationException ex)
            {
                return StatusCode(500, new { Message = "Lỗi xử lý dữ liệu JSON.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Đã xảy ra lỗi không xác định.", Details = ex.Message });
            }
        }
    }
}
