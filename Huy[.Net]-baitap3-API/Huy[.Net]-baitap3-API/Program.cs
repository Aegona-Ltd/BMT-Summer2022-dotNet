using Huy_.Net__baitap3_API.Data;
using Huy_.Net__baitap3_API.Models;
using Huy_.Net__baitap3_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectString = builder.Configuration["ConnectionStrings:DefaultConnection"];
var serverVersion = new MariaDbServerVersion(new Version(10, 4, 12));
builder.Services.AddDbContext<WebContext>(options => options.UseMySql(connectString, serverVersion));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<RecaptchaResult>(x =>
{
    x.BaseAddress = new Uri("https://www.google.com/recaptcha/api/siteverify");
});

builder.Services.AddScoped<ContactService,ContactServiceImp>();
builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = "localhost:1211";
});
var app = builder.Build();
//CreateDbIfNotExists
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<WebContext>();
        DbInitializer.Initializer(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}
app.UseRouting();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}"
    );
app.Run();
