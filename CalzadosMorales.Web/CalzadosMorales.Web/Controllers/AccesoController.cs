using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CalzadosMorales.Web.Controllers
{
    public class AccesoController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public AccesoController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: Muestra la vista del Login (tu pantalla morada)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Recibe el usuario y la clave cuando hacen clic en iniciar sesión
        [HttpPost]
        public async Task<IActionResult> Login(string usuario, string clave)
        {
            // Valida contra la base de datos usando tu servicio
            var user = _usuarioService.ValidarUsuario(usuario, clave);

            if (user != null)
            {
                // Crea los permisos (claims) para la sesión por cookies
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, user.Nombre),
                    new Claim(ClaimTypes.Role, user.Rol.Nombre)
                };

                // --- CAMBIO AQUÍ: Usamos "AdminCookie" en lugar del esquema por defecto ---
                var claimsIdentity = new ClaimsIdentity(claims, "AdminCookie");
                await HttpContext.SignInAsync("AdminCookie", new ClaimsPrincipal(claimsIdentity));

                // Redirige según el rol que venga de tu base de datos
                if (user.Rol.Nombre == "Administrador")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Venta");
                }
            }

            // Si falla, muestra el error en la vista
            ViewData["Error"] = "Usuario o contraseña incorrectos, o cuenta inactiva.";
            return View();
        }

        // Cierra la sesión del administrador
        public async Task<IActionResult> Logout()
        {
            // --- CAMBIO AQUÍ: Cerramos específicamente la cookie del admin ---
            await HttpContext.SignOutAsync("AdminCookie");
            return RedirectToAction("Login", "Acceso");
        }
    }
}