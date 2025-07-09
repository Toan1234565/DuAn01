using DuAn2811_.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Server._2811._2004.Controllers
{
    public class LocSanPham : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7247/api/locsanpham"; // Đảm bảo URL đúng

        public LocSanPham(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View(); // Trang chủ để hiển thị các tùy chọn lọc
        }

        [HttpGet]
        public async Task<IActionResult> LocSanPhamNhieu(int? maDanhMuc, int? maHangSanXuat, decimal? giatu, decimal? giaden, int? pageIndex = 1, int pageSize = 20)
        {
            string endpoint = $"{_apiBaseUrl}/GetSanPham?";
            if (maDanhMuc.HasValue)
            {
                endpoint += $"maDanhMuc={maDanhMuc}&";
            }
            if (maHangSanXuat.HasValue)
            {
                endpoint += $"maHangSanXuat={maHangSanXuat}&";
            }
            if (giatu.HasValue)
            {
                endpoint += $"giatu={giatu}&";
            }
            if (giaden.HasValue)
            {
                endpoint += $"giaden={giaden}&";
            }
            endpoint += $"pageIndex={pageIndex}&pageSize={pageSize}";

            // Loại bỏ dấu '&' thừa ở cuối nếu có tham số
            if (endpoint.EndsWith("&"))
            {
                endpoint = endpoint.TrimEnd('&');
            }

            return await FetchProducts(endpoint);
        }
        private async Task<IActionResult> FetchProducts(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = $"Lỗi API: Không thể lấy dữ liệu. Status Code: {(int)response.StatusCode}";
                    return View("Error"); // Tạo một view Error để hiển thị lỗi
                }

                var responseData = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(responseData))
                {
                    ViewBag.ErrorMessage = "API trả về dữ liệu rỗng.";
                    return View("Error"); // Hiển thị lỗi nếu dữ liệu rỗng
                }

                var products = JsonConvert.DeserializeObject<ComboView>(responseData);

                if (products == null || products.AllSanPham == null)
                {
                    ViewBag.ErrorMessage = "Không có sản phẩm nào.";
                    return View("EmptyProducts"); // Tạo một view EmptyProducts để hiển thị khi không có sản phẩm
                }

                return View("SanPhamList", products); // Truyền dữ liệu đến view SanPhamList
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
    }
}
