using pruevaDB1.Components.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace pruevaDB1.Components.Model
{
    public class Inscripcion
    {
        [Key]
        public int IdInscripcion { get; set; }
        [Required]
        public int NumeroDorsal { get; set; }
        [ForeignKey("Atleta")]
        public int AtletaId { get; set; }
        public Atleta? Atleta { get; set; }
        [ForeignKey("Carrera")]
        public int CarreraId { get; set; }
        public Carrera? Carrera { get; set; }
        public List<TiempoParcial> TiemposParciales { get; set; } = new List<TiempoParcial>();
    }
}