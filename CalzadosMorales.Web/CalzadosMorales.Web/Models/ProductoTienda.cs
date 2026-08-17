namespace CalzadosMorales.Web.Models
{
    public class ProductoTienda
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string ImagenesUnidas { get; set; } // Aquí guardaremos "img1|img2|img3"

    }
}
