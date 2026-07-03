using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SplitwiseBackend.Data;
using SplitwiseBackend.Models;

namespace SplitwiseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CuentasController(AppDbContext context)
        {
            _context = context;
        }

        //GET: api/Cuentas trae todas las cuentas de gastos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuenta>>> GetCuentas()
        {
            return await _context.Cuentas.ToListAsync();
        }

        //POST: api/Cuentas Crear una nueva cuenta
        [HttpPost]
        public async Task<ActionResult<Cuenta>> PostCuenta(Cuenta cuenta)
        {
            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCuentas), new { id = cuenta.Id }, cuenta);
        } 

        //GET: api/Cuentas/1/calcular (nuevo endpoint matemático)
        [HttpGet("{id}/calcular")]
        public async Task<ActionResult<ResumenCuentaDto>> CalcularDivisionCuenta(int id)
        {
            // Busca la cuenta en la base de datos
            var cuenta = await _context.Cuentas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cuenta == null)
            {
                return NotFound($"No se encontró la cuenta con ID {id}.");
            }

            // Trae todos los consumos amarrados a esta cuenta e incluir los datos del usuario
            var consumos = await _context.Consumos
                .Include(c => c.Usuario)
                .Where(c => c.IdCuenta == id)
                .ToListAsync();

            // Arma el molde del resumen inicial
            var resumen = new ResumenCuentaDto
            {
                CuentaId = cuenta.Id,
                Descripcion = cuenta.Descripcion,
                TotalConsumosNP = consumos.Sum(c => c.Precio)
            };

            // Calcula el factor de la propina
            decimal factorPropina = 1 + (cuenta.PropinaPorcentaje / 100);
            resumen.TotalConsumosP = resumen.TotalConsumosNP * factorPropina;

            // Agrupa los consumos por usuario
            var consumosAgrupados = consumos
                .GroupBy(c => new { c.IdUsuario, Nombre = c.Usuario != null ? c.Usuario.Nombre : "Usuario Desconocido" });

            foreach (var grupo in consumosAgrupados)
            {
                decimal totalSinPropina = grupo.Sum(c => c.Precio);
                decimal propinaIndividual = totalSinPropina * (cuenta.PropinaPorcentaje / 100);

                resumen.DesglosePorUsuario.Add(new DetalleUsuarioDto
                {
                    UsuarioId = grupo.Key.IdUsuario,
                    NombreUsuario = grupo.Key.Nombre,
                    TotalIndividualSP = totalSinPropina,
                    PropinaCorrespondiente = propinaIndividual,
                    PropinaIndivualCP = totalSinPropina + propinaIndividual
                });
            }

            return Ok(resumen);
        } 

    } 
} 