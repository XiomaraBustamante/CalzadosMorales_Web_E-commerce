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

        [Column("nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo debe contener letras y espacios.")]
        public string Nombre { get; set; }

        [Column("usuario")]
        public string UserLogin { get; set; }

        // Se quita [Required] y se hace nullable (?) para que no falle al actualizar sin cambiar la clave
        [Column("clave")]
        public string? Clave { get; set; }

        [Column("id_rol")]
        public int IdRol { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;

        [ForeignKey("IdRol")]
        public virtual Rol? Rol { get; set; }
    }
}