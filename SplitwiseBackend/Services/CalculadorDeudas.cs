using SplitwiseBackend.Models;

namespace SplitwiseBackend.Services
{
    // Clase para representar la transferencia final entre dos usuarios
    public class TransaccionDeuda
    {
        public int IdDeudor { get; set; }
        public int IdAcreedor { get; set; }
        public decimal Monto { get; set; }
    }

    // Clase interna para manejar los saldos dinámicos en el algoritmo
    internal class DeudaUsuario
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
    }

    public class CalculadorDeudas
    {
        //Método que calcula los balances netos
        public Dictionary<int, decimal> CalcularBalances(List<Consumo> consumos, List<Usuario> usuarios)
        {
            Dictionary<int, decimal> balances = new Dictionary<int, decimal>();

            foreach (var usuario in usuarios)
            {
                balances[usuario.Id] = 0;
            }

            foreach (var consumo in consumos)
            {
                if (balances.ContainsKey(consumo.IdUsuario))
                {
                    balances[consumo.IdUsuario] += consumo.Precio;
                }

                if (consumo.EsCompartido)
                {
                    if (usuarios.Count > 0)
                    {
                        decimal costoPorPersona = consumo.Precio / usuarios.Count;
                        foreach (var usuario in usuarios)
                        {
                            if (balances.ContainsKey(usuario.Id))
                            {
                                balances[usuario.Id] -= costoPorPersona;
                            }
                        }
                    }
                }
                else
                {
                    if (balances.ContainsKey(consumo.IdUsuario))
                    {
                        balances[consumo.IdUsuario] -= consumo.Precio;
                    }
                }
            }

            return balances;
        }

        //Método que simplifica los balances y genera la lista de transferencias mínimas
        public List<TransaccionDeuda> SimplificarDeudas(Dictionary<int, decimal> balances)
        {
            var transacciones = new List<TransaccionDeuda>();

            // se usa la clase interna DeudaUsuario para permitir modificar la propiedad Monto libremente
            var deudores = balances
                .Where(b => b.Value < 0)
                .Select(b => new DeudaUsuario { Id = b.Key, Monto = -b.Value })
                .OrderByDescending(b => b.Monto)
                .ToList();

            var acreedores = balances
                .Where(b => b.Value > 0)
                .Select(b => new DeudaUsuario { Id = b.Key, Monto = b.Value })
                .OrderByDescending(b => b.Monto)
                .ToList();

            int i = 0;
            int j = 0;

            while (i < deudores.Count && j < acreedores.Count)
            {
                var deudor = deudores[i];
                var acreedor = acreedores[j];

                decimal pago = Math.Min(deudor.Monto, acreedor.Monto);

                if (pago > 0.01m)
                {
                    transacciones.Add(new TransaccionDeuda
                    {
                        IdDeudor = deudor.Id,
                        IdAcreedor = acreedor.Id,
                        Monto = Math.Round(pago, 2)
                    });
                }

                //restamos de forma limpia el pago en nuestras variables locales
                deudor.Monto -= pago;
                acreedor.Monto -= pago;

                if (deudor.Monto <= 0.01m) i++;
                if (acreedor.Monto <= 0.01m) j++;
            }

            return transacciones;
        }
    }
}