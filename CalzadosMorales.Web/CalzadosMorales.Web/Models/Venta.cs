using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalzadosMorales.Web.Models
{
    [Table("venta")]
    public class Venta
    {
        [Key]
        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Required]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("total", TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [Column("estado")]
        [StringLength(20)]
        public string Estado { get; set; } = "REGISTRADA";

        [Column("tipo_comprobante")]
        [StringLength(20)]
        public string TipoComprobante { get; set; } = "Boleta";

        [Column("serie")]
        [StringLength(5)]
        public string Serie { get; set; } = "B001";

        [Column("numero")]
        [StringLength(20)]
        public string Numero { get; set; } = "";

        [Column("origen")]
        [StringLength(10)]
        public string Origen { get; set; } = "WEB";

        [Column("metodo_pago")]
        [StringLength(20)]
        public string MetodoPago { get; set; } = "Efectivo";

        [Column("codigo_sincronizacion")]
        [StringLength(100)]
        public string CodigoSincronizacion { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }
    }
}
