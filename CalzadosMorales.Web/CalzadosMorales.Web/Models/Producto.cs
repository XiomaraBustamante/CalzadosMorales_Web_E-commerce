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

        [Required]
        [Column("nombre")]
        [StringLength(150)]
        public string Nombre { get; set; }

        [Column("descripcion")]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Column("id_color")]
        public int? IdColor { get; set; }

        [Column("id_material")]
        public int? IdMaterial { get; set; }

        [Required]
        [Column("precio", TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [Required]
        [Column("id_categoria")]
        public int IdCategoria { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;

        // --- ESTOS CAMPOS NUEVOS GUARDAN EL TEXTO QUE TRAE TU PROCEDIMIENTO ALMACENADO ---
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

    }
}