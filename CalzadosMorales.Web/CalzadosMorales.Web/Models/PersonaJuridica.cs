using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalzadosMorales.Web.Models
{
    [Table("persona_juridica")]
    public class PersonaJuridica
    {
        [Key, ForeignKey("Cliente")]
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("razon_social")]
        [StringLength(60)]
        public string RazonSocial { get; set; }

        [Column("repre_legal")]
        [StringLength(100)]
        public string RepreLegal { get; set; }

        [Column("ruc")]
        [StringLength(11)]
        public string Ruc { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
