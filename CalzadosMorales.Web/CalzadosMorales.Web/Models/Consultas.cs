namespace CalzadosMorales.Web.Models
{
    // 1. Para sp_AdminAnalisisHorarioVentas
    public class AnalisisHorarioVM
    {
        public int HoraDelDia { get; set; }
        public string BloqueHorario { get; set; }
        public int NumeroDeVentas { get; set; }
        public decimal MontoRecaudado { get; set; }
    }

    // 2. Para sp_AdminHistorialGeneralVentas
    public class ReporteVentaVM
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string Vendedor { get; set; }
        public string Cliente { get; set; }
        public string Comprobante { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
    }

    // 3. Para sp_ConsultaStockFiltros
    public class ConsultaStockVM
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string NombreCategoria { get; set; }
        public string NombreTalla { get; set; }
        public string NombreColor { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string IndicadorEstado { get; set; }
    }

    // 4. Para sp_AdminReporteVentasPorFechas y sp_AdminSumatoriaVentasRango
    public class ReporteVentasRangoVM
    {
        public string Fecha { get; set; }
        public string Vendedor { get; set; }
        public string Cliente { get; set; }
        public string Comprobante { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
    }
} 