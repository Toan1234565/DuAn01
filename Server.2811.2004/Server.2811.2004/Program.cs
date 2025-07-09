using Microsoft.EntityFrameworkCore;
using Server._2811._2004.Models;

var builder = WebApplication.CreateBuilder(args);

// **Add services to the container**
builder.Services.AddControllersWithViews();

// **Configure DbContext for SQL Server**
builder.Services.AddDbContext<TmdtContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// **Add HttpClient (optional, if needed for API calls)**
builder.Services.AddHttpClient();

// **Configure Session**
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session expires after 30 minutes
    options.Cookie.HttpOnly = true; // Prevent access to cookie via JavaScript for security
    options.Cookie.IsEssential = true; // Ensure session cookies are required
});

var app = builder.Build();

// **Configure the HTTP request pipeline**
if (!app.Environment.IsDevelopment())
{
    // Use error handler in production
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Use HTTP Strict Transport Security
}
else
{
    // **Developer exception page for debugging**
    app.UseDeveloperExceptionPage();
}

// Enable redirection to HTTPS
app.UseHttpsRedirection();

// Serve static files (CSS, JS, images, etc.)
app.UseStaticFiles();

// Enable session middleware
app.UseSession();

// Routing middleware
app.UseRouting();

// Authorization middleware
app.UseAuthorization();

// Define default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=QuanLySanPham}/{action=GetAllProducts}/{id?}");

app.Run();
