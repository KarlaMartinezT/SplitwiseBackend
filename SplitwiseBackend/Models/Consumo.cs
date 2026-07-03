namespace SplitwiseBackend.Models
{
    public class Consumo
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdCuenta { get; set; }
        public string PlatilloNombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public Usuario? Usuario { get; set; }
        public Cuenta? Cuenta { get; set; }

    }
}
