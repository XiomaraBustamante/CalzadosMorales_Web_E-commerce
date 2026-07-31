using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalzadosMorales.Web.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("nombre")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Column("usuario")]
        [StringLength(50)]
        public string UserLogin { get; set; }

        [Required]
        [Column("clave")]
        [StringLength(255)]
        public string Clave { get; set; }

        [Required]
        [Column("id_rol")]
        public int IdRol { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;

        [ForeignKey("IdRol")]
        public virtual Rol Rol { get; set; }
    }
}
