using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalzadosMorales.Web.Models
{
    [Table("productos")]
    public class Producto
    {
        [Key]
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
        [Column("nombre")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("id_color")]
        public int? IdColor { get; set; }

        [Column("id_material")]
        public int? IdMaterial { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser un valor válido mayor a 0.")]
        [Column("precio", TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una categoría válida.")]
        [Column("id_categoria")]
        public int IdCategoria { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;

        // --- CAMPOS NO MAPEADOS PARA PROCEDIMIENTOS ALMACENADOS Y VISTAS ---
        [NotMapped]
        public string CategoriaNombre { get; set; }

        [NotMapped]
        public string ColorNombre { get; set; }

        [NotMapped]
        public string MaterialTipo { get; set; }

        [NotMapped]
        public string Talla { get; set; }

        [NotMapped]
        public int Stock { get; set; }

        [NotMapped]
        public List<ProductoTalla> ListaTallasStock { get; set; } = new List<ProductoTalla>();

        [NotMapped]
        public List<ProductoImagen> ListaImagenes { get; set; } = new List<ProductoImagen>();
    }
}