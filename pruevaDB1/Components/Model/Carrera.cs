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
        public string Ubicacion { get; set; }

        [JsonIgnore]
        public List<Inscripcion>? Corredores { get; set; }

        [JsonIgnore]
        public List<PuntosdeControl>? PuntosControl { get; set; }


        public Carrera() { 
            this.Corredores = new List<Inscripcion>();
            this.PuntosControl = new List<PuntosdeControl>();
        }    
    }
}
