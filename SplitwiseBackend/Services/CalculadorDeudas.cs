using SplitwiseBackend.Models;

namespace SplitwiseBackend.Services
{
    public class CalculadorDeudas
    {
        public Dictionary<int, decimal> CalcularBalances(List<Consumo> consumos, List<Usuario> usuarios)
        {
            Dictionary<int, decimal> balances = new Dictionary<int, decimal>();

            //Inicializar el balance de cada usuario en 0
            foreach (var usuario in usuarios)
            {
                balances[usuario.Id] = 0;
            }

            //Recorrer cada consumo registrado para sumar pagos y restar deudas
            foreach (var consumo in consumos)
            {
                //A la persona que pagó el platillo se le suma el total a favor
                if (balances.ContainsKey(consumo.IdUsuario))
                {
                    balances[consumo.IdUsuario] += consumo.Precio;
                }

                //Restar la parte que le toca a cada quien
                if (consumo.EsCompartido)
                {
                    // Si es compartido, el precio se divide entre todos los usuarios
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
                    // Si es individual, por ahora se le descuenta a quien lo pagó
                    if (balances.ContainsKey(consumo.IdUsuario))
                    {
                        balances[consumo.IdUsuario] -= consumo.Precio;
                    }
                }
            }

            return balances;
        }
    }
}