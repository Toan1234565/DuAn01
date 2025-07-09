using DuAn2811_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Server._2811._2004.Services;

var builder = WebApplication.CreateBuilder(args);
// Thêm dịch vụ vào container
builder.Services.AddDbContext<TmdtContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.MaxDepth = 64; // Tăng giới hạn độ sâu
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // chi dinh thoi gian co the hoat dong 20p
    options.Cookie.HttpOnly = true;//  Khi được đặt thành , nó ngăn JavaScript phía máy khách truy cập cookie phiên, thêm một lớp bảo mật chống lại các cuộc tấn công cross-site scripting (XSS)
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();
// Đăng ký dịch vụ ElasticSearchService
builder.Services.AddSingleton<ElasticSearchService>();
var app = builder.Build();

// Cấu hình pipeline HTTP request
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Giá trị HSTS mặc định là 30 ngày
}
else
{
    app.UseDeveloperExceptionPage(); // Trang lỗi chi tiết khi phát triển
}





app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Thêm middleware xác thực
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=QuanLySanPham}/{action=GetAllProducts}/{id?}");

app.Run();
