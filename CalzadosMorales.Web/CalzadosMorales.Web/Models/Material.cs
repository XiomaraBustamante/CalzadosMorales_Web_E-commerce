using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalzadosMorales.Web.Models
{
    [Table("materiales")]
    public class Material
    {
        [Key]
        [Column("id_material")]
        public int IdMaterial { get; set; }

        [Required]
        [Column("tipo")]
        [StringLength(80)]
        public string Tipo { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;
    }
}
