using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // ==========================================
        // ACCIONES - PERSONAS NATURALES
        // ==========================================

        // GET: /Cliente/Naturales
        public IActionResult Naturales()
        {
            var lista = _clienteService.ObtenerPersonasNaturales();
            return View(lista);
        }

        // GET: /Cliente/ObtenerNaturalPorId (Para AJAX / Editar)
        [HttpGet]
        public IActionResult ObtenerNaturalPorId(int id)
        {
            var cliente = _clienteService.ObtenerPersonaNaturalPorId(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Json(cliente);
        }

        // POST: /Cliente/RegistrarNatural
        [HttpPost]
        public IActionResult RegistrarNatural(string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion)
        {
            _clienteService.RegistrarPersonaNatural(dni, genero, nombre, apellido, telefono, email, direccion);
            return RedirectToAction("Naturales");
        }

        // POST: /Cliente/ActualizarNatural
        [HttpPost]
        public IActionResult ActualizarNatural(int idCliente, string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion)
        {
            _clienteService.ActualizarPersonaNatural(idCliente, dni, genero, nombre, apellido, telefono, email, direccion);
            return RedirectToAction("Naturales");
        }

        // POST: /Cliente/CambiarEstadoNatural
        [HttpPost]
        public IActionResult CambiarEstadoNatural(int idCliente, bool estado)
        {
            _clienteService.CambiarPersonaNatural(idCliente, estado);
            return RedirectToAction("Naturales");
        }

        // ==========================================
        // ACCIONES - PERSONAS JURÍDICAS
        // ==========================================

        // GET: /Cliente/Juridicas
        public IActionResult Juridicas()
        {
            var lista = _clienteService.ObtenerPersonasJuridicas();
            return View(lista);
        }

        // GET: /Cliente/ObtenerJuridicaPorId (Para AJAX / Editar)
        [HttpGet]
        public IActionResult ObtenerJuridicaPorId(int id)
        {
            var cliente = _clienteService.ObtenerPersonaJuridicaPorId(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Json(cliente);
        }

        // POST: /Cliente/RegistrarJuridica
        [HttpPost]
        public IActionResult RegistrarJuridica(string ruc, string razonSocial, string repreLegal, string telefono, string email, string direccion)
        {
            _clienteService.RegistrarPersonaJuridica(ruc, razonSocial, repreLegal, telefono, email, direccion);
            return RedirectToAction("Juridicas");
        }

        // POST: /Cliente/ActualizarJuridica
        [HttpPost]
        public IActionResult ActualizarJuridica(int idCliente, string ruc, string razonSocial, string repreLegal, string telefono, string email, string direccion)
        {
            _clienteService.ActualizarPersonaJuridica(idCliente, ruc, razonSocial, repreLegal, telefono, email, direccion);
            return RedirectToAction("Juridicas");
        }

        // POST: /Cliente/CambiarEstadoJuridica
        [HttpPost]
        public IActionResult CambiarEstadoJuridica(int idCliente, bool estado)
        {
            _clienteService.CambiarEstadoPersonaJuridica(idCliente, estado);
            return RedirectToAction("Juridicas");
        }
    }
}