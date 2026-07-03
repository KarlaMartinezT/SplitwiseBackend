using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SplitwiseBackend.Data;
using SplitwiseBackend.Models;

namespace SplitwiseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsumosController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/Consumos (Traer todos los consumos registrados)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consumo>>> GetConsumos()
        {
            return await _context.Consumos.ToListAsync();
        }

        // 2. POST: api/Consumos (Registrar lo que comió alguien, ej: "Tacos", $150)
        [HttpPost]
        public async Task<ActionResult<Consumo>> PostConsumo(Consumo consumo)
        {
            _context.Consumos.Add(consumo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConsumos), new { id = consumo.Id }, consumo);
        }
    }
}