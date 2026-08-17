using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
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
                return Json(new { success = false, message = "Usuario no encontrado." });
            }
            return Json(usuario);
        }

        // POST: /Usuario/Registrar
        [HttpPost]
        public IActionResult Registrar(Usuario usuario)
        {
            // Evita conflictos si el ID viene con un valor residual al crear
            ModelState.Remove("IdUsuario");

            // Validar las reglas del modelo ([Required], [StringLength], etc.)
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { success = false, message = "Por favor, corrija los siguientes errores:", errors = errores });
            }

            try
            {
                _usuarioService.RegistrarUsuario(usuario.Nombre, usuario.UserLogin, usuario.Clave, usuario.IdRol);
                return Json(new { success = true, message = "¡Usuario registrado correctamente!" });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = "Error al registrar el usuario: " + ex.Message });
            }
        }

        // POST: /Usuario/Actualizar
        [HttpPost]
        public IActionResult Actualizar(Usuario usuario)
        {
            // Omitir siempre la validación de clave al actualizar (ya que no se modifica en este form)
            ModelState.Remove("Clave");

            if (usuario.IdUsuario <= 0)
            {
                return Json(new { success = false, message = "El ID del usuario es inválido." });
            }

            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { success = false, message = "Por favor, corrija los siguientes errores:", errors = errores });
            }

            try
            {
                _usuarioService.ActualizarUsuario(usuario.IdUsuario, usuario.Nombre, usuario.UserLogin, usuario.IdRol);
                return Json(new { success = true, message = "¡Usuario actualizado correctamente!" });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = "Error al actualizar el usuario: " + ex.Message });
            }
        }

        // POST: /Usuario/CambiarEstado
        [HttpPost]
        public IActionResult CambiarEstado(int idUsuario, bool estado)
        {
            try
            {
                _usuarioService.CambiarEstadoUsuario(idUsuario, estado);
                return Json(new { success = true, message = "Estado actualizado correctamente." });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = "Error al cambiar el estado: " + ex.Message });
            }
        }
    }
}