using CalzadosMorales.Web.Repositorio;
using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddTransient<AdminRepository>();
builder.Services.AddTransient<AdminService>();



builder.Services.AddScoped<CalzadosMorales.Web.Repositorio.MaestroRepository>();
builder.Services.AddScoped<CalzadosMorales.Web.Servicios.MaestroService>();


builder.Services.AddScoped<ProductoRepository>();
builder.Services.AddScoped<ProductoService>();

builder.Services.AddScoped<CalzadosMorales.Web.Repositorio.ClienteRepository>();
builder.Services.AddScoped<CalzadosMorales.Web.Servicios.ClienteService>();

builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();


builder.Services.AddScoped<ReporteRepository>();
builder.Services.AddScoped<ReporteService>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuario/Login"; // Redirige aquí si no está logueado
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
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
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
