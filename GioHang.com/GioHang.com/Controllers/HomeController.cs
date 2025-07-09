using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GioHang.com.Models;
using System.Net.Http;

namespace GioHang.com.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    private readonly HttpClient _httpclient;
    public HomeController( HttpClient httpclient)
    {
       
        _httpclient = httpclient;
    }
    [HttpPost]
    public async Task<IActionResult> DangNhap(string soDienThoai, string matKhau)
    {
        // Tạo dữ liệu request
        var requestData = new
        {
            SoDienThoai = soDienThoai,
            MatKhau = matKhau
        };

        // Gọi API đăng nhập
        var response = await _httpclient.PostAsJsonAsync("https://localhost:7247/api/TaiKhoanAPI/DangNhap", requestData);

        if (response.IsSuccessStatusCode)
        {
            // Lấy dữ liệu phản hồi từ API
            var result = await response.Content.ReadFromJsonAsync<object>();
            return View(result); // Truyền dữ liệu sang view giỏ hàng
        }
        else
        {
            // Xử lý lỗi (ví dụ: thông tin đăng nhập không đúng)
            var error = await response.Content.ReadFromJsonAsync<object>();
            ViewBag.Error = error;
            return View("DangNhap");
        }
    }
}
