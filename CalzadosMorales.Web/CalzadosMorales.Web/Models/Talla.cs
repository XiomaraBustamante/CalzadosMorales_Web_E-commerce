using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalzadosMorales.Web.Models
{
    [Table("tallas")]
    public class Talla
    {
        [Key]
        [Column("id_talla")]
        public int IdTalla { get; set; }

        [Required]
        [Column("nombre")]
        [StringLength(20)]
        public string Nombre { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;
    }
}
