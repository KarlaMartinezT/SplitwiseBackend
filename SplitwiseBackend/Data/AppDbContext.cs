
using Microsoft.EntityFrameworkCore;
using SplitwiseBackend.Models;
using System.Collections.Generic;

namespace SplitwiseBackend.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Consumo> Consumos { get; set; }
    }
}
