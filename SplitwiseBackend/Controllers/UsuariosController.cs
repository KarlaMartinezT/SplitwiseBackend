using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SplitwiseBackend.Data;
using SplitwiseBackend.Models;

namespace SplitwiseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // <-- Esto le avisa a Swagger que existimos
    public class UsuariosController : ControllerBase // <-- Cambió a ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/Usuarios (Traer todos los usuarios)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // 2. POST: api/Usuarios (Crear un usuario nuevo)
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.Id }, usuario);
        }
    }
}