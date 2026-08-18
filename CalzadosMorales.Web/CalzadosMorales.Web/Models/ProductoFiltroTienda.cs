using System.Collections.Generic;

namespace CalzadosMorales.Web.Models
{
    public class ProductoViewModel
    {
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public string categoria_nombre { get; set; }
        public string color_nombre { get; set; }
        public string material_tipo { get; set; }
        public int stock_total { get; set; }
        public string imagenes_unidas { get; set; } // Aquí vienen las imágenes separadas por '|'
    }

    public class CategoriaViewModel
    {
        public int id_categoria { get; set; }
        public string nombre { get; set; }
    }

    public class ColorViewModel
    {
        public int id_color { get; set; }
        public string nombre { get; set; }
    }

    public class MaterialViewModel
    {
        public int id_material { get; set; }
        public string tipo { get; set; }
    }

    public class TallaViewModel
    {
        public int id_talla { get; set; }
        public string nombre { get; set; }
    }

    public class CatalogoCompletoViewModel
    {
        public List<ProductoViewModel> ListaProductos { get; set; }
        public List<CategoriaViewModel> ListaCategorias { get; set; }
        public List<ColorViewModel> ListaColores { get; set; }
        public List<MaterialViewModel> ListaMateriales { get; set; }
        public List<TallaViewModel> ListaTallas { get; set; }
    }
}