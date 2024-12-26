using BLL.DAL;
using BLL.Models;
using BLL.Services;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// AppSettings 
var appSettings = builder.Configuration.GetSection(nameof(AppSettings));
appSettings.Bind(new AppSettings());

//IoC Container:
string connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<Db>(options =>options.UseSqlServer(connectionString));
builder.Services.AddScoped<IService<Role, RoleModel>, RoleService>();
builder.Services.AddScoped<IService<User, UserModel>, UserService>();
builder.Services.AddScoped<IService<Thanks, ThanksModel>, ThanksService>();
builder.Services.AddScoped<IService<Category, CategoryModel>, CategoryService>();
builder.Services.AddScoped<IService<Tag, TagModel>, TagService>();

//Authentication:
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Users/Login";
    options.AccessDeniedPath = "/Users/Login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});
     
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

//Authenticaiton:
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
