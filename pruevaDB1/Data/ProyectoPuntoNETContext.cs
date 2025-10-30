using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPuntoNET.Data
{
    public class ProyectoPuntoNETContext : DbContext
    {
        public ProyectoPuntoNETContext (DbContextOptions<ProyectoPuntoNETContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoPuntoNET.Data.Atleta> Atleta { get; set; } = default!;
    }
}
