using DuAn2811_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Server._2811._2004.model1;
using System.Text.Json;
using System.Threading.Tasks;

namespace DuAn2811_.Controllers
{
    public class ChiTietSanPham : Controller // Corrected class name
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7247/api/chitiet";

        public ChiTietSanPham(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet] // Attribute to specify this is a GET request
        public async Task<IActionResult> GetChiTiet(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/GetChiTietsanpham?id={id}");

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = $"Lỗi API: Không thể lấy dữ liệu. Status Code: {(int)response.StatusCode}";
                    return View("Error");
                }

                var responseData = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(responseData))
                {
                    ViewBag.ErrorMessage = "API trả về dữ liệu rỗng.";
                    return View("Error");
                }

                var product = JsonConvert.DeserializeObject<SanPhamViewModel>(responseData);

                if (product == null)
                {
                    ViewBag.ErrorMessage = "Không tìm thấy sản phẩm.";
                    return View("Error");
                }

              

                return View(product);
            }
            catch (HttpRequestException)
            {
                ViewBag.ErrorMessage = "Lỗi kết nối đến API.";
                return View("Error");
            }
            catch (JsonSerializationException)
            {
                ViewBag.ErrorMessage = "Lỗi xử lý dữ liệu JSON từ API.";
                return View("Error");
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi không xác định.";
                return View("Error");
            }
        }
        public async Task<IActionResult> DaXem()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/GetSanPhamDaXem");
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = $"Lỗi API: Không thể lấy danh sách sản phẩm đã xem. Status Code: {(int)response.StatusCode}";
                    return View("Error");
                }

                var responseData = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(responseData))
                {
                    ViewBag.ErrorMessage = "API trả về dữ liệu rỗng cho danh sách sản phẩm đã xem.";
                    return View("Error");
                }

                var recentlyViewedProducts = System.Text.Json.JsonSerializer.Deserialize<List<SanPhamViewModel>>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Create a ComboView instance and assign the list to its RecentlyViewed property
                var comboViewModel = new ComboView
                {
                    RecentlyViewed = recentlyViewedProducts
                    // Add other properties of ComboView if needed
                };

                return View("DaXem", comboViewModel); // Correct - passing ComboView
            }
            catch (HttpRequestException)
            {
                ViewBag.ErrorMessage = "Lỗi kết nối đến API khi lấy danh sách sản phẩm đã xem.";
                return View("Error");
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi không xác định khi lấy danh sách sản phẩm đã xem.";
                return View("Error");
            }
        }
    }
}

