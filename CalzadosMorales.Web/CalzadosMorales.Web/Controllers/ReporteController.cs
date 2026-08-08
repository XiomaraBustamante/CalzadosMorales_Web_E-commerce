using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Repositorio;
using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers
{
    [Authorize]
    public class ReporteController : Controller
    {
        private readonly ReporteService _reporteService;
        private readonly MaestroRepository _maestroRepository; // Inyectamos el repositorio maestro

        public ReporteController(ReporteService reporteService, MaestroRepository maestroRepository)
        {
            _reporteService = reporteService;
            _maestroRepository = maestroRepository;
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
            // Usamos ListarCategorias() de tu MaestroRepository para llenar el combobox
            ViewBag.ListaCategorias = _maestroRepository.ListarCategorias();

            ViewBag.IdCategoriaSeleccionada = idCategoria;
            ViewBag.NombreTallaSeleccionada = nombreTalla;

            var modelo = _reporteService.ConsultarStockFiltros(idCategoria, nombreTalla);
            return View(modelo);
        }

        // 4 y 5. Vista para Reporte de Ventas por Fechas y su Sumatoria
        public IActionResult VentasPorFechas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            // Si es la primera vez que entra y no hay fechas, enviamos valores nulos para no mostrar datos por defecto
            if (!fechaInicio.HasValue || !fechaFin.HasValue)
            {
                ViewBag.FechaInicio = "";
                ViewBag.FechaFin = "";
                ViewBag.BusquedaRealizada = false;
                return View(new List<ReporteVentasRangoVM>());
            }

            DateTime inicio = fechaInicio.Value;
            DateTime fin = fechaFin.Value;

            if (inicio > fin)
            {
                var temp = inicio;
                inicio = fin;
                fin = temp;
            }

            ViewBag.FechaInicio = inicio.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fin.ToString("yyyy-MM-dd");
            ViewBag.BusquedaRealizada = true;

            var listaVentas = _reporteService.ReporteVentasPorFechas(inicio, fin);
            decimal sumatoriaTotal = _reporteService.ObtenerSumatoriaVentasRango(inicio, fin);

            ViewBag.SumatoriaTotal = sumatoriaTotal;

            return View(listaVentas);
        }
    }
}