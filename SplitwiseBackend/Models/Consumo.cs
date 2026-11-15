using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SplitwiseBackend.Models
{
    public class Consumo
    {
        [Key]
        public int Id { get; set; }

        [Column("usuario_id")]
        [ForeignKey("Usuario")] //la llave del objeto Usuario
        public int IdUsuario { get; set; }

        [Column("cuenta_id")]
        [ForeignKey("Cuenta")]  //la llave del objeto Cuenta
        public int IdCuenta { get; set; }

        [Column("platillo_nombre")]
        public string PlatilloNombre { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public Usuario? Usuario { get; set; }
        public Cuenta? Cuenta { get; set; }

        // Si es true, se divide entre todos los participantes de la cuenta.
        // Si es false, le pertenece únicamente al IdUsuario que lo registró.
        public bool EsCompartido { get; set; } = true;
    }
}