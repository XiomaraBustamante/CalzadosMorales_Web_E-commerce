using CalzadosMorales.Web.Repositorio;
using CalzadosMorales.Web.Servicios;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

var cloudinaryAccount = new Account(
    builder.Configuration["CloudinarySettings:CloudName"],
    builder.Configuration["CloudinarySettings:ApiKey"],
    builder.Configuration["CloudinarySettings:ApiSecret"]
);
var cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary);


builder.Services.AddControllersWithViews();

builder.Services.AddTransient<CalzadosMorales.Web.Datos.ConexionBD>();
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
        options.LoginPath = "/Acceso/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

var app = builder.Build();


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
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();