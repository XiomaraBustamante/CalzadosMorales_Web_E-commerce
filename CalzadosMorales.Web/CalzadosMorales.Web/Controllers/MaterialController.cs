using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers
{
    [Authorize]
    public class MaterialController : Controller
    {
        private readonly MaestroService _maestroService;

        public MaterialController(MaestroService maestroService)
        {
            _maestroService = maestroService;
        }

        public IActionResult Index()
        {
            try
            {
                var lista = _maestroService.ObtenerMateriales();
                return View(lista);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(new List<Models.Material>());
            }
        }

        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var material = _maestroService.ObtenerMaterialPorId(id);
                if (material == null)
                    return Json(new { success = false, message = "Material no encontrado." });

                return Json(new { success = true, data = material });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Registrar(string tipo)
        {
            try
            {
                _maestroService.GuardarMaterial(tipo);
                return Json(new { success = true, message = "¡Material registrado con éxito!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Actualizar(int id, string tipo)
        {
            try
            {
                _maestroService.ActualizarMaterial(id, tipo);
                return Json(new { success = true, message = "¡Material actualizado con éxito!" });
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
                _maestroService.CambiarEstadoMaterial(id, estado);
                return Json(new { success = true, message = "¡Estado cambiado con éxito!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}