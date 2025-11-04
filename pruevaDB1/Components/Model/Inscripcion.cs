using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pruevaDB1.Components.Model
{
    public class Inscripcion
    {
        [Key]
        public int IdInscripcion { get; set; }
        public int NumeroDorsal { get; set; }

        [JsonIgnore]
        public int Atleta { get; set; }

        [JsonIgnore]
        public int Carrera { get; set; }
        public List<TiemposParciales> TiempoParcial { get; set; }

        public Inscripcion()
        {
            this.TiempoParcial = new List<TiemposParciales>();
        }
    }
}
