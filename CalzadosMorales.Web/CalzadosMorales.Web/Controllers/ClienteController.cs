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
            var cliente = _clienteService.ObtenerPersonaNaturalPorId(id);
            if (cliente == null) return NotFound();
            return Json(cliente);
        }

        [HttpPost]
        public IActionResult RegistrarNatural(string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion)
        {
            _clienteService.RegistrarPersonaNatural(dni, genero, nombre, apellido, telefono, email, direccion);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ActualizarNatural(int idCliente, string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion)
        {
            _clienteService.ActualizarPersonaNatural(idCliente, dni, genero, nombre, apellido, telefono, email, direccion);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CambiarEstadoNatural(int idCliente, bool estado)
        {
            _clienteService.CambiarEstadoPersonaNatural(idCliente, estado);
            return RedirectToAction("Index");
        }

        // ==========================================
        // ACCIONES - PERSONAS JURÍDICAS
        // ==========================================

        [HttpGet]
        public IActionResult ObtenerJuridicaPorId(int id)
        {
            var cliente = _clienteService.ObtenerPersonaJuridicaPorId(id);
            if (cliente == null) return NotFound();
            return Json(cliente);
        }

        [HttpPost]
        public IActionResult RegistrarJuridica(string ruc, string razonSocial, string repreLegal, string telefono, string email, string direccion)
        {
            _clienteService.RegistrarPersonaJuridica(ruc, razonSocial, repreLegal, telefono, email, direccion);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ActualizarJuridica(int idCliente, string ruc, string razonSocial, string repreLegal, string telefono, string email, string direccion)
        {
            _clienteService.ActualizarPersonaJuridica(idCliente, ruc, razonSocial, repreLegal, telefono, email, direccion);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CambiarEstadoJuridica(int idCliente, bool estado)
        {
            _clienteService.CambiarEstadoPersonaJuridica(idCliente, estado);
            return RedirectToAction("Index");
        }
    }
}