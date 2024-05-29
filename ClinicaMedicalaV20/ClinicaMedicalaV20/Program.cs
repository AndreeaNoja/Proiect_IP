using ClinicaMedicalaV20.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
// Adaug? IHttpContextAccessor la serviciile aplica?iei
builder.Services.AddHttpContextAccessor();

// Adaug? serviciile de autentificare ?i configureaz? schema de autentificare bazat? pe cookie-uri
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Index"; // Sau unde ai pagina de login
        options.LogoutPath = "/Medics/Logout"; // Sau unde gestionezi logout-ul
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session services to the container
builder.Services.AddDistributedMemoryCache(); // Necessary for session state
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Adaug? acest middleware înainte de UseAuthorization
app.UseSession(); // Make sure UseSession is after UseRouting and before UseEndpoints
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
