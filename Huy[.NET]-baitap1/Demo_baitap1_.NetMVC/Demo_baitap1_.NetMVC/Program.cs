using Demo_baitap1_.NetMVC.Models;
using Demo_baitap1_.NetMVC.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddScoped<AccountService, AccountServiceImp>();
builder.Services.AddScoped<ContactService, ContactServiceImp>();
builder.Services.AddDbContext<DatabaseContext>();
var app = builder.Build();
app.UseRouting();
app.UseStaticFiles();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}"
    );
app.Run();
