using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalzadosMorales.Web.Models
{
    [Table("producto_talla")]
    public class ProductoTalla
    {
        [Key, Column("id_producto", Order = 0)]

        public int IdProducto { get; set; }

        [Key, Column("id_talla", Order = 1)]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una talla válida.")]
        public int IdTalla { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, 100000, ErrorMessage = "El stock no puede ser un número negativo.")]
        [Column("stock")]
        public int Stock { get; set; } = 0;

        [ForeignKey("IdProducto")]
        public virtual Producto? Producto { get; set; }

        [ForeignKey("IdTalla")]
        public virtual Talla? Talla { get; set; }
    }
}