using System.ComponentModel.DataAnnotations;

namespace ProyectoPuntoNET.Model
{
    public class Carrera
    {
        [Key]
        public int idCarrera { get; set; }
        public string nombre { get; set; }
        public DateOnly fecha { get; set; }
        public int cantidadPuntosControl { get; set; }
        public int cuposDisponibles { get; set; }
        public string mapa { get; set; }
        public List<Participacion>? corredores { get; set; }
    }
}
