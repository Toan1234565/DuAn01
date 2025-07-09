using Microsoft.EntityFrameworkCore; // Thư viện cần thiết để làm việc với Entity Framework Core
using TaiKhoan.Models; // Thư viện chứa các model được sử dụng trong ứng dụng
using TaiKhoan.MailUtils; // Import nếu sử dụng dịch vụ Email
using Microsoft.OpenApi.Models; // Thư viện để cấu hình Swagger

var builder = WebApplication.CreateBuilder(args);

// **Thêm các services** (các dịch vụ sử dụng trong ứng dụng)
// Thêm các controller (API endpoints) vào ứng dụng
builder.Services.AddControllers();

// Kích hoạt Endpoints API Explorer, cần thiết để hiển thị Swagger
builder.Services.AddEndpointsApiExplorer();

// Cấu hình Swagger để tự động sinh tài liệu API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaiKhoan API", // Tiêu đề tài liệu API
        Version = "v1" // Phiên bản của API
    });
});

// **Thêm DbContext**
// Sử dụng Entity Framework Core để kết nối cơ sở dữ liệu SQL Server
builder.Services.AddDbContext<TmdtContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Chuỗi kết nối được định nghĩa trong appsettings.json (DefaultConnection)

// **Cấu hình Authentication bằng Cookie**
builder.Services.AddAuthentication("Cookies") // Thêm dịch vụ Authentication sử dụng Cookies
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/login"; // Đường dẫn nếu người dùng chưa đăng nhập
        options.AccessDeniedPath = "/denied"; // Đường dẫn khi truy cập bị từ chối
    });

// **Đăng ký dịch vụ tùy chỉnh**
// Đăng ký EmailService như là dịch vụ cho IEmailService, vòng đời là Scoped
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build(); // Xây dựng ứng dụng từ các dịch vụ đã cấu hình

// **Cấu hình HTTP request pipeline**
if (app.Environment.IsDevelopment()) // Nếu ứng dụng trong môi trường phát triển
{
    app.UseSwagger(); // Kích hoạt Swagger
    app.UseSwaggerUI(); // Cung cấp giao diện Swagger UI để thử nghiệm API
}

app.UseHttpsRedirection(); // Tự động chuyển hướng từ HTTP sang HTTPS
app.UseStaticFiles(); // Cho phép ứng dụng phục vụ các file tĩnh như CSS, JavaScript, hình ảnh

app.UseRouting(); // Kích hoạt định tuyến middleware

// **Thêm middleware Authentication và Authorization**
app.UseAuthentication(); // Kích hoạt middleware xác thực (Authentication)
app.UseAuthorization(); // Kích hoạt middleware phân quyền (Authorization)

app.MapControllers(); // Kích hoạt ánh xạ các controller với các endpoint đã định nghĩa

app.Run(); // Chạy ứng dụng
