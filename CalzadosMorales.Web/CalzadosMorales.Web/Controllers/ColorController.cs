using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers
{
    [Authorize]
    public class ColorController : Controller
    {
        private readonly MaestroService _maestroService;

        public ColorController(MaestroService maestroService)
        {
            _maestroService = maestroService;
        }

        public IActionResult Index()
        {
            var lista = _maestroService.ObtenerColores();
            return View(lista);
        }

        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            var color = _maestroService.ObtenerColorPorId(id);
            return Json(color);
        }

        [HttpPost]
        public IActionResult Registrar(string nombre)
        {
            _maestroService.GuardarColor(nombre);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Actualizar(int id, string nombre)
        {
            _maestroService.ActualizarColor(id, nombre);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CambiarEstado(int id, bool estado)
        {
            _maestroService.CambiarEstadoColor(id, estado);
            return RedirectToAction("Index");
        }
    }
}