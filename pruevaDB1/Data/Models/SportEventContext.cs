using Microsoft.EntityFrameworkCore;
using ProyectoPuntoNET.Data;
using pruevaDB1.Data.Models;

namespace pruevaDB1.Data
{
    public class SportEventContext : DbContext
    {
        public SportEventContext(DbContextOptions<SportEventContext> options) : base(options) { }

        public DbSet<Atleta> Atletas { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<PuntoControl> PuntosDeControl { get; set; }
        public DbSet<TiempoParcial> TiemposParciales { get; set; }
    }
}