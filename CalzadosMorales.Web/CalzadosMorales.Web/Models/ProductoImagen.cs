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

        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La URL de la imagen es obligatoria.")]
        [StringLength(500, ErrorMessage = "La ruta de la imagen no puede superar los 500 caracteres.")]
        [Column("imagen_url")]
        public string ImagenUrl { get; set; }

        [Required(ErrorMessage = "El orden de la imagen es obligatorio.")]
        [Range(0, 100, ErrorMessage = "El orden de la imagen debe ser un número entero válido (mínimo 0).")]
        [Column("orden")]
        public int Orden { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto? Producto { get; set; }
    }
}