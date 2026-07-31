using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers
{
    public class MaterialController : Controller
    {
        private readonly MaestroService _maestroService;

        public MaterialController(MaestroService maestroService)
        {
            _maestroService = maestroService;
        }

        public IActionResult Index()
        {
            var lista = _maestroService.ObtenerMateriales();
            return View(lista);
        }

        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            var material = _maestroService.ObtenerMaterialPorId(id);
            return Json(material);
        }

        [HttpPost]
        public IActionResult Registrar(string tipo)
        {
            _maestroService.GuardarMaterial(tipo);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Actualizar(int id, string tipo)
        {
            _maestroService.ActualizarMaterial(id, tipo);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CambiarEstado(int id, bool estado)
        {
            _maestroService.CambiarEstadoMaterial(id, estado);
            return RedirectToAction("Index");
        }
    }
}