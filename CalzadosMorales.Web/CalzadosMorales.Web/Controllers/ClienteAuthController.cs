using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CalzadosMorales.Web.Controllers
{
    public class ClienteAuthController : Controller
    {
        private readonly ClienteService _clienteService;

        // Inyectamos el servicio de clientes
        public ClienteAuthController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ==========================================
        // REGISTRO DE PERSONA NATURAL (TIENDA)
        // ==========================================
        [HttpPost]
        public IActionResult RegistrarPersonaNatural(string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion, string password)
        {
            try
            {
                // Llama al servicio que exige contraseña obligatoriamente
                _clienteService.RegistrarPersonaNatural(dni, genero, nombre, apellido, telefono, email, direccion, password);

                TempData["MensajeExito"] = "¡Registro exitoso! Ya puedes iniciar sesión.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // ==========================================
        // LOGIN DE CLIENTE
        // ==========================================
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string email, string password)
        {
            try
            {
                // Validamos en la base de datos mediante el repositorio/servicio
                var cliente = _clienteService.LoginCliente(email, password);

                if (cliente != null)
                {
                    // Creamos los datos de sesión (Cookies) seguros
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, cliente.Email),
                        new Claim("IdCliente", cliente.IdCliente.ToString())
                    };

                    // Utiliza explícitamente el esquema por defecto para los clientes
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Tienda");
                }

                TempData["MensajeError"] = "Correo o contraseña incorrectos.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // ==========================================
        // CERRAR SESIÓN
        // ==========================================
        [HttpPost] 
        public async Task<IActionResult> CerrarSesion()
        {
            // Cierra la cookie por defecto del cliente
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Tienda");
        }
    }
}