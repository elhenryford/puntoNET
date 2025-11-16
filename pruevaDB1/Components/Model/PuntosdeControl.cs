using System.ComponentModel.DataAnnotations;

namespace pruevaDB1.Components.Model
{
    public class PuntosdeControl
    {
        [Key]
        public int IdPunto { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public Carrera CarreraId { get; set; }
        public List<TiemposParciales> Tiempos { get; set; }
    }
}
