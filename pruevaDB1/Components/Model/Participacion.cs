using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pruevaDB1.Components.Model
{
    public class Participacion
    {
        [Key]
        public int IdParticipacion { get; set; }

        [JsonIgnore]
        public Atleta Atleta { get; set; }

        [JsonIgnore]
        public Carrera Carrera { get; set; }
        public List<TimeSpan> Tiempos { get; set; }
        public TimeSpan TiempoFinal { get; set; }

        public Participacion()
        {
            this.Tiempos = [];
        }
    }
}
