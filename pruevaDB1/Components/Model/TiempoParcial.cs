using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace pruevaDB1.Components.Model
{
    public class TiempoParcial
    {
        [Key]
        public int IdTiempo { get; set; }
        [ForeignKey("Inscripcion")]
        public int InscripcionId { get; set; }
        public Inscripcion? Inscripcion { get; set; }
        [ForeignKey("PuntoControl")]
        public int PuntoControlId { get; set; }
        public PuntoControl? PuntoControl { get; set; }
        public DateTime HoraPaso { get; set; } = DateTime.Now;
    }
}