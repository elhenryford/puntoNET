using Microsoft.EntityFrameworkCore;
using pruevaDB1.Components.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruevaDB1.Data
{
    public class pruevaDB1Context : DbContext
    {
        public pruevaDB1Context(DbContextOptions<pruevaDB1Context> options)
            : base(options)
        {
        }

        public DbSet<Atleta> Atletas { get; set; } = default!;
        public DbSet<Carrera> Carreras { get; set; } = default!;
        public DbSet<Inscripcion> Inscripciones { get; set; } = default!;
        public DbSet<TiempoParcial> TiempoParcial { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }
    }

}