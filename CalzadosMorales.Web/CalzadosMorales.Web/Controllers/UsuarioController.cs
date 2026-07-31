using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CalzadosMorales.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // ==========================================
        // ACCIONES DE AUTENTICACIÓN (LOGIN / LOGOUT)
        // ==========================================

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string usuario, string clave)
        {
            var user = _usuarioService.ValidarUsuario(usuario, clave);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, user.Nombre),
                    new Claim(ClaimTypes.Role, user.Rol.Nombre)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ViewData["Error"] = "Usuario o contraseña incorrectos, o cuenta inactiva.";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Usuario");
        }

        // ==========================================
        // GESTIÓN DE USUARIOS (CRUD)
        // ==========================================

        // GET: /Usuario/Index
        public IActionResult Index()
        {
            var lista = _usuarioService.ListarUsuarios();
            ViewBag.Roles = _usuarioService.ListarRoles(); // Para llenar el select/combo en la vista
            return View(lista);
        }

        // GET: /Usuario/ObtenerPorId (Para AJAX / Editar)
        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            var usuario = _usuarioService.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Json(usuario);
        }

        // POST: /Usuario/Registrar
        [HttpPost]
        public IActionResult Registrar(string nombre, string usuario, string clave, int idRol)
        {
            _usuarioService.RegistrarUsuario(nombre, usuario, clave, idRol);
            return RedirectToAction("Index");
        }

        // POST: /Usuario/Actualizar
        [HttpPost]
        public IActionResult Actualizar(int idUsuario, string nombre, string usuario, int idRol)
        {
            _usuarioService.ActualizarUsuario(idUsuario, nombre, usuario, idRol);
            return RedirectToAction("Index");
        }

        // POST: /Usuario/CambiarEstado
        [HttpPost]
        public IActionResult CambiarEstado(int idUsuario, bool estado)
        {
            _usuarioService.CambiarEstadoUsuario(idUsuario, estado);
            return RedirectToAction("Index");
        }
    }
}
