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
        public string Discapacidades { get; set; }

        [JsonIgnore]
        public List<Participacion>? Participaciones { get; set; }

        public Atleta() { this.Participaciones = []; }


    }
}
