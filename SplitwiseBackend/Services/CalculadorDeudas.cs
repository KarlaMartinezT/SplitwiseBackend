using SplitwiseBackend.Models;

namespace SplitwiseBackend.Services
{
    public class CalculadorDeudas
    {
        public Dictionary<int, decimal> CalcularBalances(List<Consumo> consumos, List<Usuario> usuarios)
        {
            Dictionary<int, decimal> balances = new Dictionary<int, decimal>();

            // 1. Inicializar el balance de cada usuario en 0
            foreach (var usuario in usuarios)
            {
                balances[usuario.Id] = 0;
            }

            // 2. Recorrer cada consumo registrado para sumar y restar
            foreach (var consumo in consumos)
            {
                // Supongamos que tu modelo Consumo tiene:
                // - UsuarioId (quien pagó)
                // - Monto (cuánto costó)

                // A la persona que pagó le sumamos el total del gasto a su favor
                if (balances.ContainsKey(consumo.IdUsuario))
                {
                    balances[consumo.IdUsuario] += consumo.Precio;
                }
            }

            return balances;
        }
    }
}