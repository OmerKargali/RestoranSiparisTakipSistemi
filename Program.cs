using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"));
});

// Add services to the container.

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
});
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Hesap/Giris";
        option.AccessDeniedPath = "/Hesap/AccessDenied";
        //option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        //option.SlidingExpiration = false;

    });
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
