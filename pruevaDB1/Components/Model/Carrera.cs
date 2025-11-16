using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace pruevaDB1.Components.Model
{
    public class Carrera
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCarrera { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public DateTime HoraInicio { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public int Cupos { get; set; }
        public int cantSensores { get; set; }
        public int inscGanador { get; set; } = 0;
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}