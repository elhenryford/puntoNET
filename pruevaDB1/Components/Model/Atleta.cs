using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace pruevaDB1.Components.Model
{
    public class Atleta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAtleta { get; set; }
        [Required]
        public string Mail { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public int Edad { get; set; }
        public int NumeroDorsal { get; set; } 
        public int ChipID { get; set; } 

        public string? Discapacidades { get; set; }
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}