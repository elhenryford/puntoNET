using pruevaDB1.Components.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace pruevaDB1.Data.Models
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
    }
}