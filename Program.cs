using HarnyCardApplication.ApplicationContext;
using HarnyCardApplication.Repositories.Implementattions;
using HarnyCardApplication.Repositories.Interfaces;
using HarnyCardApplication.Services.Implementations;
using HarnyCardApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ContextClass>(x => x.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
// Add services to the container.

builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardService, CardService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<INetworkRepository, NetworkRepository>();
builder.Services.AddScoped<INetworkService, NetworkService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IRoleRepository , RoleRepository>();
builder.Services.AddScoped<IRoleService , RoleService>();

builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IManagerService , ManagerService>();

builder.Services.AddScoped<IUserRepository , UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
   .AddCookie(config =>
   {
       config.LoginPath = "/Home/Index";
       config.LogoutPath = "/Home/Index";
       config.Cookie.Name = "AirTimeApplication";
   });
builder.Services.AddAuthorization();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "AirtimeApp.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(100);
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
