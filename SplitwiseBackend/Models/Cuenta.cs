namespace SplitwiseBackend.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal TotalCuentas { get; set; }
        public decimal PropinaPorcentaje { get; set; }

    }
}
