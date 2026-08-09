using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: /Cliente/Index (Vista unificada con pestañas)
        public IActionResult Index()
        {
            ViewBag.ListaNaturales = _clienteService.ObtenerPersonasNaturales();
            ViewBag.ListaJuridicas = _clienteService.ObtenerPersonasJuridicas();
            return View();
        }

        // ==========================================
        // ACCIONES - PERSONAS NATURALES
        // ==========================================

        [HttpGet]
        public IActionResult ObtenerNaturalPorId(int id)
        {
            try
            {
                var cliente = _clienteService.ObtenerPersonaNaturalPorId(id);
                if (cliente == null) return Json(new { success = false, message = "Cliente no encontrado." });
                return Json(new { success = true, data = cliente });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult RegistrarNatural(string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion)
        {
            try
            {
                _clienteService.RegistrarPersonaNatural(dni, genero, nombre, apellido, telefono, email, direccion);
                return Json(new { success = true, message = "Persona natural registrada exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ActualizarNatural(int idCliente, string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion)
        {
            try
            {
                _clienteService.ActualizarPersonaNatural(idCliente, dni, genero, nombre, apellido, telefono, email, direccion);
                return Json(new { success = true, message = "Persona natural actualizada exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CambiarEstadoNatural(int idCliente, bool estado)
        {
            try
            {
                _clienteService.CambiarEstadoPersonaNatural(idCliente, estado);
                return Json(new { success = true, message = "Estado actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // ==========================================
        // ACCIONES - PERSONAS JURÍDICAS
        // ==========================================

        [HttpGet]
        public IActionResult ObtenerJuridicaPorId(int id)
        {
            try
            {
                var cliente = _clienteService.ObtenerPersonaJuridicaPorId(id);
                if (cliente == null) return Json(new { success = false, message = "Cliente no encontrado." });
                return Json(new { success = true, data = cliente });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult RegistrarJuridica(string ruc, string razonSocial, string repreLegal, string telefono, string email, string direccion)
        {
            try
            {
                _clienteService.RegistrarPersonaJuridica(ruc, razonSocial, repreLegal, telefono, email, direccion);
                return Json(new { success = true, message = "Persona jurídica registrada exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ActualizarJuridica(int idCliente, string ruc, string razonSocial, string repreLegal, string telefono, string email, string direccion)
        {
            try
            {
                _clienteService.ActualizarPersonaJuridica(idCliente, ruc, razonSocial, repreLegal, telefono, email, direccion);
                return Json(new { success = true, message = "Persona jurídica actualizada exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CambiarEstadoJuridica(int idCliente, bool estado)
        {
            try
            {
                _clienteService.CambiarEstadoPersonaJuridica(idCliente, estado);
                return Json(new { success = true, message = "Estado actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}