using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pruevaDB1.Components.Model
{
    public class Inscripcion
    {
        [Key]
        public int IdInscripcion { get; set; }
        public int NumeroDorsal { get; set; }
<<<<<<< Updated upstream
=======
        public int ChipId { get; set; } // Nuevo campo
        public int Posicion { get; set; } = 0;
        public List<TiempoParcial> TiemposParciales { get; set; } = new List<TiempoParcial>();
        public TimeSpan TiempoTotal { get; set; }
>>>>>>> Stashed changes

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
