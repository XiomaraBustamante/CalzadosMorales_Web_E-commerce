using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers
{
    public class TallaController : Controller
    {
        private readonly MaestroService _maestroService;

        public TallaController(MaestroService maestroService)
        {
            _maestroService = maestroService;
        }

        public IActionResult Index()
        {
            var lista = _maestroService.ObtenerTallas();
            return View(lista);
        }

        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            var talla = _maestroService.ObtenerTallaPorId(id);
            return Json(talla);
        }

        [HttpPost]
        public IActionResult Registrar(string nombre)
        {
            _maestroService.GuardarTalla(nombre);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Actualizar(int id, string nombre)
        {
            _maestroService.ActualizarTalla(id, nombre);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CambiarEstado(int id, bool estado)
        {
            _maestroService.CambiarEstadoTalla(id, estado);
            return RedirectToAction("Index");
        }
    }
}