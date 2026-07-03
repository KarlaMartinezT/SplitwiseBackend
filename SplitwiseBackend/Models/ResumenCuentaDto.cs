namespace SplitwiseBackend.Models
{
    public class ResumenCuentaDto
    {
        public int CuentaId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal TotalConsumosNP { get; set; } //sin propina
        public decimal TotalConsumosP { get; set; } //con propina

        public List<DetalleUsuarioDto> DesglosePorUsuario { get; set; } = new();
    }
    public class DetalleUsuarioDto
    {
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public decimal TotalIndividualSP { get; set; } //sin propina
        public decimal PropinaCorrespondiente { get; set; }

        public decimal PropinaIndivualCP { get; set; } //con propina

    }
}
