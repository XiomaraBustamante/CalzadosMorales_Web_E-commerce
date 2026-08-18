using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalzadosMorales.Web.Models
{
    [Table("cliente")]
    public class Cliente
    {
        [Key]
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("direccion")]
        [StringLength(100)]
        public string Direccion { get; set; }

        [Column("telefono")]
        [StringLength(9)]
        public string Telefono { get; set; }

        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }

      
        [Column("password")]
        [StringLength(255)]
        public string? Password { get; set; }
        // ---------------------------

        [Column("fecha_registro", TypeName = "date")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Column("estado")]
        public bool Estado { get; set; } = true;
    }
}
