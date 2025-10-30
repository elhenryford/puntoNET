using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProyectoPuntoNET.Data
{

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