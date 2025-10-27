using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPuntoNET.Data
{
    public class Evento
    {
        [Key]
        public int EventoId { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        // Lista de atletas opcional si querés relación bidireccional
        [NotMapped]
        public List<Atleta> Atletas { get; set; } = new List<Atleta>();
    }
}
