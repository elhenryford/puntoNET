using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pruevaDB1.Components.Model;

namespace pruevaDB1.Data
{
    public class pruevaDB1Context : DbContext
    {
        public pruevaDB1Context (DbContextOptions<pruevaDB1Context> options)
            : base(options)
        {
        }

        public DbSet<Atleta> Atleta { get; set; } = default!;
        public DbSet<Carrera> Carrera { get; set; } = default!;
        public DbSet<pruevaDB1.Components.Model.Inscripcion> Participacion { get; set; } = default!;
    }
}
