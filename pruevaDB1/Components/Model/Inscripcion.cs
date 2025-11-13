using pruevaDB1.Components.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace pruevaDB1.Components.Model
{
    [Table("Inscripcion")]
    public class Inscripcion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInscripcion { get; set; }
        public int AtletaId { get; set; }
        public int CarreraId { get; set; }
        public int NumeroDorsal { get; set; }
        public int ChipId { get; set; } // Nuevo campo
        public int Posicion { get; set; } = 0;
        public List<TiempoParcial> TiemposParciales { get; set; } = new List<TiempoParcial>();
        public TimeSpan TiempoTotal { get; set; }

    }

}
