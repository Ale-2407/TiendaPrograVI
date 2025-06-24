using Tienda_PrograVI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // soporte de sesiones
builder.Services.AddHttpContextAccessor(); // acceso a HttpContext

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "clientes",
    pattern: "{controller=Clientes}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "productos",
    pattern: "Productos/{action=Index}/{id?}",
    defaults: new { controller = "Producto" });

app.Run();
