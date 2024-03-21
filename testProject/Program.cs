using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using testProject.Models;
using testProject.Repositories.ExchangeRateRepository;
using testProject.Repositories.TimeRepository;
using testProject.Repositories.UserRepository;
using testProject.Repositories.WeatherRepository;
using testProject.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Register AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Register HttpClient
builder.Services.AddHttpClient();

// Register services
builder.Services.AddTransient<IWeatherService>(provider =>
    new WeatherService("d5b3ec31a5faec857dce94b61d6c38b9"));

builder.Services.AddTransient<IExchangeRateService>(provider =>
    new ExchangeRateService("a81c8c14a03efcbe364d2f364fa09d5b"));

builder.Services.AddTransient<ITimeService>(provider =>
    new TimeService("FuR8ypUy1GykP/7VnMoMRQ==masYEkGr5aHnmRKZ"));

builder.Services.AddTransient<IUserCollection, SqlUserRepository>();

builder.Services.AddTransient<IPostApiService, PostApiService>();

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Access/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");
//pattern: "{controller=Post}/{action=Index}/{id?}");
app.Run();
