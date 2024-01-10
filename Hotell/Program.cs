using Hotell;
using Hotell.Services;
using Hotell.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NuGet.Protocol.Core.Types;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(100);
});



builder.Services.AddHttpClient<IAccountService, AccountService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("WebApiUrl") ?? string.Empty);
    
});
builder.Services.AddHttpClient<IRoomService, RoomService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("WebApiUrl") ?? string.Empty);
});
builder.Services.AddHttpClient<IBookingService, BookingService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("WebApiUrl") ?? string.Empty);
});
builder.Services.AddHttpClient<IAdminService, AdminService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("WebApiUrl") ?? string.Empty);
});

builder.Services.AddHttpContextAccessor();
builder.Services.TryAddScoped<ISessionManager, SessionManager>();

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

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
