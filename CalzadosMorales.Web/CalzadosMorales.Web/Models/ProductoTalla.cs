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
        public int IdTalla { get; set; }

        [Required]
        [Column("stock")]
        public int Stock { get; set; } = 0;

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }

        [ForeignKey("IdTalla")]
        public virtual Talla Talla { get; set; }
    }
}
