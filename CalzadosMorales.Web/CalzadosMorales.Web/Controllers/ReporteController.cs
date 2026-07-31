using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers
{
    public class ReporteController : Controller
    {
        private readonly ReporteService _reporteService;

        public ReporteController(ReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        // Vista general de reportes / panel
        public IActionResult Index()
        {
            return View();
        }

        // 1. Vista o endpoint para Análisis Horario
        public IActionResult AnalisisHorario()
        {
            var modelo = _reporteService.ObtenerAnalisisHorarioVentas();
            return View(modelo);
        }

        // 2. Vista para el Historial General de Ventas
        public IActionResult HistorialVentas()
        {
            var modelo = _reporteService.ListarHistorialGeneralVentas();
            return View(modelo);
        }

        // 3. Vista para Consulta de Stock con Filtros
        public IActionResult ConsultaStock(int idCategoria = 0, string nombreTalla = "")
        {
            ViewBag.IdCategoriaSeleccionada = idCategoria;
            ViewBag.NombreTallaSeleccionada = nombreTalla;

            var modelo = _reporteService.ConsultarStockFiltros(idCategoria, nombreTalla);
            return View(modelo);
        }

        // 4 y 5. Vista para Reporte de Ventas por Fechas y su Sumatoria
        public IActionResult VentasPorFechas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            // Si las fechas son nulas por defecto, podemos mandar el mes actual o el día de hoy
            DateTime inicio = fechaInicio ?? DateTime.Today.AddDays(-30);
            DateTime fin = fechaFin ?? DateTime.Today;

            ViewBag.FechaInicio = inicio.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fin.ToString("yyyy-MM-dd");

            var listaVentas = _reporteService.ReporteVentasPorFechas(inicio, fin);
            decimal sumatoriaTotal = _reporteService.ObtenerSumatoriaVentasRango(inicio, fin);

            ViewBag.SumatoriaTotal = sumatoriaTotal;

            return View(listaVentas);
        }
    }
}
