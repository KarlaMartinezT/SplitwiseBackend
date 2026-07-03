using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SplitwiseBackend.Models
{
    public class Cuenta
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;

        [Column ("total_cuenta")] //mapeo a la columna real d la bd
        public decimal TotalCuentas { get; set; }

        [Column("propina_porcentaje")] //mapeo a la columna real d la bd
        public decimal PropinaPorcentaje { get; set; }

    }
}
