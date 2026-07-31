using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalzadosMorales.Web.Models
{
    [Table("producto_imagen")]
    public class ProductoImagen
    {
        [Key]
        [Column("id_imagen")]
        public int IdImagen { get; set; }

        [Required]
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Required]
        [Column("imagen_url")]
        [StringLength(500)]
        public string ImagenUrl { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }
    }
}
