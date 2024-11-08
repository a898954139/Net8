using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.Implementations;
using Bulky.DataAccess.Repository.Interfaces;
using Bulky.Models.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var service = builder.Services;

service
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();
service
    .AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("BulkyDB")));
service
    .Configure<AppSettingsModel>(builder.Configuration.GetSection("ConnectionStrings"));
service
    .AddSingleton<IDapperRepository<Category>, DapperRepository>();
service
    .AddScoped<IDatabaseFactory, DatabaseFactory>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
