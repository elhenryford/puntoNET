using System.ComponentModel.DataAnnotations;
namespace pruevaDB1.Data.Models
{
    public class Carrera
    {
        [Key]
        public int IdCarrera { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Ubicacion { get; set; } = string.Empty;

        public ICollection<PuntoControl> PuntosDeControl { get; set; } = new List<PuntoControl>();
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}