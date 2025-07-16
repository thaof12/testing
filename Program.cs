using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SP6.Models;
//using LittleFishStation.Models;

var builder = WebApplication.CreateBuilder(args);

// Thêm đăng ký DbContext cho DI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Thêm MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware cơ bản
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Route cho Area
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Route mặc định: mở Admin/Bida/Ban
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Bida}/{action=Ban}/{id?}");


app.Run();
