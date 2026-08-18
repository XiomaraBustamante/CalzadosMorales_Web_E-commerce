using CalzadosMorales.Web.Repositorio;
using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers 
{

    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult Dashboard()
        {
            // Mandamos los indicadores (KPIs) a la vista mediante ViewBag
            ViewBag.CajaHoy = _adminService.ObtenerIngresosHoy();
            ViewBag.CantidadVentasHoy = _adminService.ObtenerVentasHoy();
            ViewBag.StockCritico = _adminService.ObtenerStockCritico();
            ViewBag.ClientesNuevosMes = _adminService.ObtenerClientesNuevos();
            ViewBag.TicketPromedio = _adminService.ObtenerTicketPromedio();

            // Mandamos las listas para los gráficos y la tabla de top vendedores
            ViewBag.VentasSemanales = _adminService.ObtenerVentasSemanales();
            ViewBag.StockCategoria = _adminService.ObtenerStockPorCategoria();
            ViewBag.TopVendedores = _adminService.ObtenerTopVendedores();

            return View();
        }
    }
}
