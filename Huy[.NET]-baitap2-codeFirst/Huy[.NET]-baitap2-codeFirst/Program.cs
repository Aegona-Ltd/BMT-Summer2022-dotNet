using Huy_.NET__baitap2_codeFirst.Data;
using Huy_.NET__baitap2_codeFirst.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectString = builder.Configuration["ConnectionStrings:DefaultConnection"];

var serverVersion = new MariaDbServerVersion(new Version(10, 4, 12));

builder.Services.AddDbContext<SchoolContext>(options => options.UseMySql(connectString, serverVersion));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<StudentService, StudentServiceImp>();
builder.Services.AddScoped<CourseService, CourseServiceImp>();
builder.Services.AddScoped<InstructorService, InstructorServiceImp>();
builder.Services.AddScoped<DepartmentService, DepartmentServiceImp>();
var app = builder.Build();

//CreateDbIfNotExists
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SchoolContext>();
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
