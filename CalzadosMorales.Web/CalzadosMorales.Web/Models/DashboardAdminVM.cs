namespace CalzadosMorales.Web.Models
{
    public class VentaSemanaVM
    {
        public string FechaEtiqueta { get; set; }
        public decimal TotalDia { get; set; }
    }

    public class StockCategoriaVM
    {
        public string Categoria { get; set; }
        public int TotalStock { get; set; }
    }

    public class TopVendedorVM
    {
        public string Vendedor { get; set; }
        public int CantidadOperaciones { get; set; }
        public decimal MontoTotal { get; set; }
        public int Variedad { get; set; }
    }
}