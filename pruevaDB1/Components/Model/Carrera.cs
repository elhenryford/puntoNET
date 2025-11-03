using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pruevaDB1.Components.Model
{
    public class Carrera
    {
        [Key]
        public int IdCarrera { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public int CantidadPuntosControl { get; set; }
        public int CuposDisponibles { get; set; }
        public string Mapa { get; set; }

        [JsonIgnore]
        public List<Participacion>? Corredores { get; set; }

        public Carrera() { this.Corredores = []; }    
    }
}
