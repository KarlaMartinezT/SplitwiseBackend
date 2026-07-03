using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SplitwiseBackend.Models
{
    public class Consumo
    {
        [Key] //indica llave primaria d la tabla
        public int Id { get; set; }

        [Column("usuario_id")] //mapeo a la columna real d la bd
        public int IdUsuario { get; set; }

        [Column("cuenta_id")] //mapeo a la columna real d la bd
        public int IdCuenta { get; set; }

        [Column("platillo_nombre")]
        public string PlatilloNombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }

        [ForeignKey("IdUsuario")] //indica que la propiedad Usuario es una clave foranea que hace referencia a la propiedad IdUsuario
        public Usuario? Usuario { get; set; }

        [ForeignKey("IdCuenta")] //indica que la propiedad Cuenta es una clave foranea que hace referencia a la propiedad IdCuenta
        public Cuenta? Cuenta { get; set; }

    }
}
