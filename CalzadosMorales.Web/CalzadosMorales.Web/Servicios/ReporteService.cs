using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Repositorio;

namespace CalzadosMorales.Web.Servicios
{
    public class ReporteService
    {
        private readonly ReporteRepository _reporteRepository;

        public ReporteService(ReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        public List<AnalisisHorarioVM> ObtenerAnalisisHorarioVentas()
        {
            return _reporteRepository.ObtenerAnalisisHorarioVentas();
        }

        public List<ReporteVentaVM> ListarHistorialGeneralVentas()
        {
            return _reporteRepository.ListarHistorialGeneralVentas();
        }

        public List<ConsultaStockVM> ConsultarStockFiltros(int idCategoria, string nombreTalla)
        {
            return _reporteRepository.ConsultarStockFiltros(idCategoria, nombreTalla);
        }

        public List<ReporteVentasRangoVM> ReporteVentasPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return _reporteRepository.ReporteVentasPorFechas(fechaInicio, fechaFin);
        }

        public decimal ObtenerSumatoriaVentasRango(DateTime fechaInicio, DateTime fechaFin)
        {
            return _reporteRepository.ObtenerSumatoriaVentasRango(fechaInicio, fechaFin);
        }
    }
}