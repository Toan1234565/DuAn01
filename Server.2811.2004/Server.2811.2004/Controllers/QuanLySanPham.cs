using DuAn2811_.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DuAn2811_.Controllers
{
    public class QuanLySanPham : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7247/api/QuanLySanPhamAPI";

        public QuanLySanPham(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/GetAllProducts?pageIndex={pageIndex}&pageSize={pageSize}");

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, new { Message = "Lỗi API: Không thể lấy dữ liệu." });
                }

                var responseData = await response.Content.ReadAsStringAsync();

                // Kiểm tra dữ liệu trước khi Deserialize
                if (string.IsNullOrWhiteSpace(responseData))
                {
                    return StatusCode(500, new { Message = "API trả về dữ liệu rỗng." });
                }

                var products = JsonConvert.DeserializeObject<ComboView>(responseData);

                // Kiểm tra sản phẩm có hợp lệ không
                if (products == null || products.AllSanPham == null || products.AllSanPham.Count == 0)
                {
                    return View("EmptyProducts"); // Hiển thị view rỗng
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
