using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace pruevaDB1.Data.Models
{
    public class PuntoControl
    {
        [Key]
        public int IdPunto { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public int Orden { get; set; }
        [ForeignKey("Carrera")]
        public int CarreraId { get; set; }
        public Carrera? Carrera { get; set; }
    }
}