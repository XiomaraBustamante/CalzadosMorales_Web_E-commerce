using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly MaestroService _maestroService;

        public CategoriaController(MaestroService maestroService)
        {
            _maestroService = maestroService;
        }

        public IActionResult Index()
        {
            try
            {
                var lista = _maestroService.ObtenerCategorias();
                return View(lista);
            }
            catch (Exception ex)
            {
                // Si falla al listar, puedes mandar una lista vacía o manejar el error en la vista
                TempData["Error"] = ex.Message;
                return View(new List<Models.Categoria>());
            }
        }

        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var categoria = _maestroService.ObtenerCategoriaPorId(id);
                if (categoria == null)
                    return Json(new { success = false, message = "Categoría no encontrada." });

                return Json(new { success = true, data = categoria });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Registrar(string nombre)
        {
            try
            {
                _maestroService.GuardarCategoria(nombre);
                return Json(new { success = true, message = "¡Categoría registrada con éxito!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Actualizar(int id, string nombre)
        {
            try
            {
                _maestroService.ActualizarCategoria(id, nombre);
                return Json(new { success = true, message = "¡Categoría actualizada con éxito!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CambiarEstado(int id, bool estado)
        {
            try
            {
                _maestroService.CambiarEstadoCategoria(id, estado);
                return Json(new { success = true, message = "¡Estado cambiado con éxito!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}