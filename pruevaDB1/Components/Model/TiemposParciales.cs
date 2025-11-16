using System.ComponentModel.DataAnnotations;

namespace pruevaDB1.Components.Model
{
    public class TiemposParciales
    {
        [Key]
        public int IdTiempo { get; set; }
        public DateTime HoraPaso { get; set; }
        public Inscripcion InscripcionId { get; set; }
        public PuntosdeControl PuntoControlId { get; set; }
    }
}
