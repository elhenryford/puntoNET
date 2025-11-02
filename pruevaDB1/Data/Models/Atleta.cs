using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPuntoNET.Data
{
    public class Atleta
    {
        [Key]
        public int Numero { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public int Edad { get; set; }

        [NotMapped]
        public List<string> Discapacidades { get; set; } = new List<string>();

        public string? ChipId { get; set; }
        public string? Categoria { get; set; }

        public int? EventoId { get; set; }           // Clave foránea
        public Evento? Evento { get; set; }          // Propiedad de navegación

        public Atleta()
        {
            Discapacidades = new List<string>();
        }
    }

    
}

