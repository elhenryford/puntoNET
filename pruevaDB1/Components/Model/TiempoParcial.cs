using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pruevaDB1.Components.Model
{
   
    public class TiempoParcial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTiempo { get; set; }

        [ForeignKey("Inscripcion")]
        public int InscripcionId { get; set; }
        public int Puesto { get; set; }
        public int NumeroDorsal { get; set; }
        public int ChipID { get; set; }
        public Inscripcion? Inscripcion { get; set; }

        public DateTime HoraPaso { get; set; }

    }
}
