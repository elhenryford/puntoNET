using pruevaDB1.Data.Models;
using System.ComponentModel.DataAnnotations;
namespace pruevaDB1.Components.Model
{
    public class Atleta
    {
        [Key]
        public int IdAtleta { get; set; }
        [Required]
        public string Mail { get; set; } = string.Empty;
        [Required]
        public int Edad { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        
        public string? Discapacidades { get; set; }
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}