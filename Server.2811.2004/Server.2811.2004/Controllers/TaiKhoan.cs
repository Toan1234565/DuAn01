using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Server._2811._2004.Models;
using Microsoft.EntityFrameworkCore;
using Server._2811._2004.Models1._1;
using Microsoft.IdentityModel.Tokens;


namespace Server._2811._2004.Controllers
{
    public class TaiKhoan : Controller
    {
        
       
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7276/api/TaiKhoanControllersAPI"; // URL API của bạn
        public TaiKhoan(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public IActionResult Index()
        {
           return View();
   
        }      
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string soDienThoai, string matKhau)
        {
            try
            {
                if (string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(matKhau))
                {
                    ViewBag.ErrorMessage = "Số điện thoại và mật khẩu không được để trống.";
                    return View();
                }

                var loginData = new { soDienThoai = soDienThoai, matKhau = matKhau };
                var json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var loginResult = JsonSerializer.Deserialize<LoginResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Lưu thông tin vào Session
                    HttpContext.Session.SetString("UserName", loginResult.TaiKhoan.TenDangNhap);

                    return RedirectToAction("GetAllProducts", "QuanLySanPham"); // Chuyển hướng đến trang chủ
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = errorContent;
                    return View();
                }
            }
            catch(HttpRequestException ex)
            {
                ViewBag.ErrorMessage = "Không thể kết nối đến máy chủ. Vui lòng thử lại sau.";
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register(string soDienThoai, string matKhau)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào không được để trống
                if (string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(matKhau))
                {
                    ViewBag.ErrorMessage = "Số điện thoại và mật khẩu không được để trống.";
                    return View();
                }

                // Tạo dữ liệu đăng ký dưới dạng JSON
                var dangKyData = new
                {
                    SoDienThoai = soDienThoai,
                    MatKhau = matKhau
                };

                var json = JsonSerializer.Serialize(dangKyData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    // Gửi yêu cầu POST đến API
                    var response = await _httpClient.PostAsync($"{_apiBaseUrl}/register", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        ViewBag.SuccessMessage = "Đăng ký thành công! Vui lòng kiểm tra email để xác thực tài khoản.";
                        return View("Login"); // Chuyển hướng đến trang Login
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = $"Đăng ký thất bại: {errorContent}";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi trong quá trình gọi API
                    ViewBag.ErrorMessage = $"Đã xảy ra lỗi: {ex.Message}";
                    return View();
                }
            }
            catch(HttpRequestException ex)
            {
                ViewBag.ErrorMessage = "Không thể kết nối đến máy chủ. Vui lòng thử lại sau.";
                return View();
            }
            
        }


    }
}