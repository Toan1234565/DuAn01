using DuAn2811_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DuAn2811_.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7247/api/QuanLySanPhamAPI";
        private readonly string _apiBaseUrl1 = "https://localhost:7247/api/LocSanPham";

        public QuanLySanPhamController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(int pageIndex = 1, int pageSize = 20, string sortOrder = "gia_asc")
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/GetAllProducts?pageIndex={pageIndex}&pageSize={pageSize}&sortOrder={sortOrder}");

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Lỗi API: Không thể lấy dữ liệu.";
                    return View("Error"); // Hoặc xử lý lỗi khác
                }

                var responseData = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(responseData))
                {
                    ViewBag.ErrorMessage = "API trả về dữ liệu rỗng.";
                    return View("Error"); // Hoặc xử lý lỗi khác
                }

                var products = JsonConvert.DeserializeObject<ComboView>(responseData);

                if (products == null || products.AllSanPham == null || products.AllSanPham.Count == 0)
                {
                    return View("EmptyProducts"); // Hiển thị view rỗng
                }

                // Truyền thông tin sắp xếp hiện tại đến View để hiển thị
                ViewBag.CurrentSort = sortOrder;

                return View(products);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.ErrorMessage = $"Lỗi kết nối đến API: {ex.Message}";
                return View("Error");
            }
            catch (JsonSerializationException ex)
            {
                ViewBag.ErrorMessage = $"Lỗi xử lý dữ liệu JSON: {ex.Message}";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Đã xảy ra lỗi không xác định: {ex.Message}";
                return View("Error");
            }
        }

        // Action để xử lý việc thay đổi thứ tự sắp xếp
        public IActionResult SortProducts(string sortOrder, int pageIndex = 1, int pageSize = 20)
        {
            // Chuyển hướng trở lại action GetAllProducts với tham số sortOrder mới
            return RedirectToAction("GetAllProducts", new { pageIndex = pageIndex, pageSize = pageSize, sortOrder = sortOrder });
        }
    }
}
