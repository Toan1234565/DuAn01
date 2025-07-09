using DuAn2811_.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Server._2811._2004.Controllers
{
    public class LocSanPham1 : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7247/api/SanPham/LocSanPham"; // Đường dẫn API mới

        public LocSanPham1(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        private async Task<IActionResult> FetchProducts(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Lỗi API: Không thể lấy dữ liệu.";
                    //return StatusCode((int)response.StatusCode, new { Message = "Lỗi API: Không thể lấy dữ liệu." });
                }

                var responseData = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(responseData))
                {
                    return StatusCode(500, new { Message = "API trả về dữ liệu rỗng." });
                }

                var products = JsonConvert.DeserializeObject<ComboView>(responseData);

                if (products == null || products.AllSanPham == null) // Sửa đổi điều kiện kiểm tra
                {
                    return View("EmptyProducts");
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

        [HttpGet("HangSanXuat")]
        public async Task<IActionResult> GetProductsByManufacturer(int maHangSanXuat, int pageIndex = 1, int pageSize = 20, string sortOrder = "gia_asc")
        {
            string endpoint = $"{_apiBaseUrl}?maHangSanXuat={maHangSanXuat}&pageIndex={pageIndex}&pageSize={pageSize}&sortOrder={sortOrder}";
            ViewBag.CurrentSortOrder = sortOrder; // Truyền thứ tự sắp xếp hiện tại sang view
            ViewBag.CurrentHangSanXuat = maHangSanXuat; // Truyền ID nhà sản xuất hiện tại sang view
            return await FetchProducts(endpoint);
        }

        // Lọc theo danh mục và sắp xếp theo giá (mặc định) hoặc ID danh mục
        [HttpGet("DanhMuc")]
        public async Task<IActionResult> GetProductsByCategory(int maDanhMuc, int pageIndex = 1, int pageSize = 20, string sortOrder = "gia_asc")
        {
            string endpoint = $"{_apiBaseUrl}?maDanhMuc={maDanhMuc}&pageIndex={pageIndex}&pageSize={pageSize}&sortOrder={sortOrder}";
            ViewBag.CurrentDanhMuc = maDanhMuc;
            ViewBag.CurrentSortOrder = sortOrder; // Truyền thứ tự sắp xếp hiện tại sang view
            return await FetchProducts(endpoint);
        }

        // Lọc theo cả danh mục và hãng sản xuất, và sắp xếp theo hãng sản xuất và mã danh mục
        [HttpGet("LocTheo")]
        public async Task<IActionResult> GetProductsByFilter(int? maDanhMuc, int? maHangSanXuat, string sortOrder = "manufacturer_category", int pageIndex = 1, int pageSize = 20)
        {
            string endpoint = $"{_apiBaseUrl}?maDanhMuc={maDanhMuc}&maHangSanXuat={maHangSanXuat}&pageIndex={pageIndex}&pageSize={pageSize}&sortOrder={sortOrder}";
            ViewBag.CurrentDanhMuc = maDanhMuc;
            ViewBag.CurrentHangSanXuat = maHangSanXuat;
            ViewBag.CurrentSortOrder = sortOrder; // Truyền thứ tự sắp xếp hiện tại sang view
            return await FetchProducts(endpoint);
        }

        // loc hang san xuat 
        [HttpGet("GetManufacturerOld")]
        public async Task<IActionResult> GetManufacturer(int maSanXuat, int pageIndex = 1, int pageSize = 20)
        {
            return await FetchProducts($"{_apiBaseUrl}?maHangSanXuat={maSanXuat}&pageIndex={pageIndex}&pageSize={pageSize}");
        }
        // loc danh muc san pham dien thoai
        [HttpGet("GetphonefilterOld")]
        public async Task<IActionResult> Getphonefilter(int maDanhMuc, int pageIndex = 1, int pageSize = 20)
        {
            return await FetchProducts($"{_apiBaseUrl}?maDanhMuc={maDanhMuc}&pageIndex={pageIndex}&pageSize={pageSize}");
        }
        // loc danh muc san pham Dong ho 
        [HttpGet("GetFilterClockOld")]
        public async Task<IActionResult> GetFilterClock(int maDanhMuc, int pageIndex = 1, int pageSize = 20)
        {
            return await FetchProducts($"{_apiBaseUrl}?maDanhMuc={maDanhMuc}&pageIndex={pageIndex}&pageSize={pageSize}");
        }
        // loc danh muc laptop 
        [HttpGet("GetLapTopfileterOld")]
        public async Task<IActionResult> GetLapTopfileter(int maDanhMuc, int pageIndex = 1, int pageSize = 20)
        {
            return await FetchProducts($"{_apiBaseUrl}?maDanhMuc={maDanhMuc}&pageIndex={pageIndex}&pageSize={pageSize}");
        }
    }
}