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

        public DbSet<Atleta> Atletas { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<TiempoParcial> TiempoParcial { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }
    }

}