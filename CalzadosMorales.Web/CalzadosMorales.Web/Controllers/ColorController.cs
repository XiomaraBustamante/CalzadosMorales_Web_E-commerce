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
            try
            {
                var lista = _maestroService.ObtenerColores();
                return View(lista);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(new List<Models.Color>());
            }
        }

        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var color = _maestroService.ObtenerColorPorId(id);
                if (color == null)
                    return Json(new { success = false, message = "Color no encontrado." });

                return Json(new { success = true, data = color });
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
                _maestroService.GuardarColor(nombre);
                return Json(new { success = true, message = "¡Color registrado con éxito!" });
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
                _maestroService.ActualizarColor(id, nombre);
                return Json(new { success = true, message = "¡Color actualizado con éxito!" });
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
                _maestroService.CambiarEstadoColor(id, estado);
                return Json(new { success = true, message = "¡Estado cambiado con éxito!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}