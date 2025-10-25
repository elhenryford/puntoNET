using Microsoft.EntityFrameworkCore;
using ProyectoPuntoNET.Data; // 👈 importa el namespace donde está la clase Atleta

namespace ProyectoPuntoNET.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // 👇 agrega esta línea si falta
        public DbSet<Atleta> Atletas { get; set; }

        public DbSet<ChipEvent> ChipEvents { get; set; }
        public DbSet<Evento> Eventos { get; set; } // si tenés una clase Evento también
    }
}
