using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProyectoPuntoNET.Data
{
    public class Evento
     {
        public int Id { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }

        public class Atleta
        {
            [Key]
            public int Numero { get; set; }
            [Required]
            public string Nombre { get; set; } = string.Empty;
            public int? Edad { get; set; }

            // Lista de discapacidades (podrías serializarla como texto si usas SQLite)
            [NotMapped]
            public List<string> Discapacidades { get; set; } = new List<string>();

            public string? ChipId { get; set; }          // Relación con el chip
            public string? Categoria { get; set; }       // Opcional

            // 👇 Relación con el evento (competencia)
            public int? EventoId { get; set; }           // Clave foránea
            public Evento? Evento { get; set; }          // Propiedad de navegación
        }
    


    public class ChipEvent
    {
        [Key]
        public long Id { get; set; }
        public string? ChipId { get; set; }
        public DateTime Timestamp { get; set; }
        public int? AthleteId { get; set; }
        public Atleta? Atleta { get; set; }
        public string? Checkpoint { get; set; }
    }
}