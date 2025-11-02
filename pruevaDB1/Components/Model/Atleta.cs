using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pruevaDB1.Components.Model
{
    public class Atleta
    {
        [Key]
        public int IdAtleta { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public int Edad { get; set; }
        public string? Discapacidades { get; set; }

        [JsonIgnore]
        public List<Participacion>? Participaciones { get; set; }
	//public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();

        public Atleta() { this.Participaciones = []; }


    }
}
