using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalzadosMorales.Web.Models
{
    [Table("colores")]
    public class Color
    {
        [Key]
        [Column("id_color")]
        public int IdColor { get; set; }

        [Required]
        [Column("nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;
    }
}
