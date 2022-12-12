using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Sportzall.Models;
using Sportzall.Repositories.Implementations;
using Sportzall.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<SportzalDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDBContext")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserControllable, UserControllable>();
builder.Services.AddScoped<IAbonementUserControllable, IAbonementUserControllable>();
builder.Services.AddScoped<ITrenerControllable, TrenerControllable>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
