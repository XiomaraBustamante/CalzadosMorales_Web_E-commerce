using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers
{
    [Authorize]
    public class TallaController : Controller
    {
        private readonly MaestroService _maestroService;

        public TallaController(MaestroService maestroService)
        {
            _maestroService = maestroService;
        }

        public IActionResult Index()
        {
            try
            {
                var lista = _maestroService.ObtenerTallas();
                return View(lista);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(new List<Models.Talla>());
            }
        }

        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var talla = _maestroService.ObtenerTallaPorId(id);
                if (talla == null)
                    return Json(new { success = false, message = "Talla no encontrada." });

                return Json(new { success = true, data = talla });
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
                _maestroService.GuardarTalla(nombre);
                return Json(new { success = true, message = "¡Talla registrada con éxito!" });
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
                _maestroService.ActualizarTalla(id, nombre);
                return Json(new { success = true, message = "¡Talla actualizada con éxito!" });
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
                _maestroService.CambiarEstadoTalla(id, estado);
                return Json(new { success = true, message = "¡Estado cambiado con éxito!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}