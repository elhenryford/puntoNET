using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoPuntoNET.Model;

namespace ProyectoPuntoNET.Data
{
    public class ProyectoPuntoNETContext : DbContext
    {
        public ProyectoPuntoNETContext (DbContextOptions<ProyectoPuntoNETContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoPuntoNET.Model.Atleta> Atleta { get; set; } = default!;
        public DbSet<ProyectoPuntoNET.Model.Carrera> Carrera { get; set; } = default!;
        public DbSet<ProyectoPuntoNET.Model.Participacion> Participacion { get; set; } = default!;
    }
}
