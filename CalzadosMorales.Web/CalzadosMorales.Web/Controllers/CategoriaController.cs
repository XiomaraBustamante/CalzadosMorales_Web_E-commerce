using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly MaestroService _maestroService;

        public CategoriaController(MaestroService maestroService)
        {
            _maestroService = maestroService;
        }

        public IActionResult Index()
        {
            var lista = _maestroService.ObtenerCategorias();
            return View(lista);
        }

        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            var categoria = _maestroService.ObtenerCategoriaPorId(id);
            return Json(categoria);
        }

        [HttpPost]
        public IActionResult Registrar(string nombre)
        {
            _maestroService.GuardarCategoria(nombre);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Actualizar(int id, string nombre)
        {
            _maestroService.ActualizarCategoria(id, nombre);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CambiarEstado(int id, bool estado)
        {
            _maestroService.CambiarEstadoCategoria(id, estado);
            return RedirectToAction("Index");
        }
    }
}