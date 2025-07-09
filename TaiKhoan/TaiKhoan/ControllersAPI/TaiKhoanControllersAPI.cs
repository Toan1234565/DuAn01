using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using TaiKhoan.Models;
using TaiKhoan.model1;
using TaiKhoan.MailUtils;

using Microsoft.EntityFrameworkCore;


namespace TaiKhoan.ControllersAPI
{
    [Route("api/TaiKhoanControllersAPI")]
    [ApiController]
    public class TaiKhoanControllersAPI : ControllerBase
    {
        private readonly TmdtContext _dbContext;
        private readonly IEmailService _emailService;       
        public TaiKhoanControllersAPI(TmdtContext context, IEmailService emailService)
        {
            _dbContext = context;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> DangNhap([FromBody] model1.LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(request.SoDienThoai) || string.IsNullOrEmpty(request.MatKhau))
            {
                return BadRequest(new { Message = "Số điện thoại và mật khẩu không được để trống." });
            }

            // Băm mật khẩu
            var hashedPassword = HashHelper.HashPassword(request.MatKhau);

            // Tìm tài khoản
            var taiKhoan = await _dbContext.TaiKHoans
                .FirstOrDefaultAsync(t => t.SoDienThoai == request.SoDienThoai && t.MatKhau == hashedPassword);

            if (taiKhoan != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, taiKhoan.TenDangNhap ?? ""),
            new Claim(ClaimTypes.Email, taiKhoan.Email ?? "")
        };

                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookies", principal); // Chỉ định schema "Cookies"

                return Ok(new { Message = "Đăng nhập thành công", taiKhoan });
            }

            return Unauthorized(new { Message = "Thông tin đăng nhập không chính xác." });
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> HoSo(int id)
        {
            var taiKhoan = await _dbContext.TaiKHoans
                .Include(tk => tk.DanhSachYeuThiches)
                .Include(tk => tk.DiaChiGiaoHangs)
                .Include(tk => tk.GioHangs)
                .FirstOrDefaultAsync(tk => tk.MaTk == id);

            if (taiKhoan == null)
            {
                return NotFound("Không tìm thấy tài khoản.");
            }

            return Ok(taiKhoan);
        }

        [HttpPost("register")]
        public async Task<IActionResult> DangKy([FromBody] TaiKHoan model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_dbContext.TaiKHoans.Any(t => t.SoDienThoai == model.SoDienThoai))
            {
                return Conflict("Số điện thoại đã được đăng ký.");
            }

            if (_dbContext.TaiKHoans.Any(t => t.Email == model.Email))
            {
                return Conflict("Email đã được sử dụng.");
            }

            var emailToken = Guid.NewGuid().ToString();
            model.EmailToken = emailToken;
            model.MatKhau = HashHelper.HashPassword(model.MatKhau);
            model.IsEmailConfirmed = false;

            try
            {
                await _emailService.SendConfirmationEmailAsync(model.Email, emailToken);
                _dbContext.TaiKHoans.Add(model);
                await _dbContext.SaveChangesAsync();

                return Ok("Tài khoản đã được tạo thành công. Vui lòng kiểm tra email để xác thực.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi xảy ra: " + ex.Message);
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            var user = _dbContext.TaiKHoans.FirstOrDefault(u => u.EmailToken == token);

            if (user != null)
            {
                user.IsEmailConfirmed = true;
                user.EmailToken = null;
                await _dbContext.SaveChangesAsync();

                return Ok("Email đã được xác nhận.");
            }
            else
            {
                return BadRequest("Token không hợp lệ.");
            }
        }
    }
}
