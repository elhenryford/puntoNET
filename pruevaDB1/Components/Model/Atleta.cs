using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pruevaDB1.Components.Model
{
    public class Atleta
    {
        [Key]
        public int IdAtleta { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string? Discapacidades { get; set; }
        public string mail { get; set; }
        public string password { get; set; }

        [JsonIgnore]
        public List<Inscripcion>? Participaciones { get; set; }

        public Atleta() { this.Participaciones = new List<Inscripcion>(); }


    }
}
