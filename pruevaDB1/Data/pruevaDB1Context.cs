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
        public DbSet<TiempoParcial> TiemposParciales { get; set; }
        public DbSet<Inscripcion> Participacion { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Evitar ciclo de eliminación en cascada entre TiempoParcial y PuntoControl
            modelBuilder.Entity<TiempoParcial>()
                .HasOne(tp => tp.Inscripcion)
                .WithMany(pc => pc.TiemposParciales)
                .HasForeignKey(tp => tp.InscripcionId)
                .OnDelete(DeleteBehavior.NoAction);

            // (Opcional) también podés asegurar que Inscripcion se maneje igual si hay conflictos:
            modelBuilder.Entity<TiempoParcial>()
                .HasOne(tp => tp.Inscripcion)
                .WithMany()
                .HasForeignKey(tp => tp.InscripcionId)
                .OnDelete(DeleteBehavior.NoAction);
        }



    }
}