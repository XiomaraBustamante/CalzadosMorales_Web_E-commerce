using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalzadosMorales.Web.Models
{
    [Table("persona_natural")]
    public class PersonaNatural
    {
        [Key, ForeignKey("Cliente")]
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("dni")]
        [StringLength(8)]
        public string Dni { get; set; }

        [Column("nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Column("apellido")]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [Column("genero")]
        public int Genero { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
