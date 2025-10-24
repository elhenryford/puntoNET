using System.ComponentModel.DataAnnotations;

namespace ProyectoPuntoNET.Model
{
    public class Participacion
    {
        [Key]
        public int idParticipacion { get; set; }
        public Atleta atleta { get; set; }
        public Carrera carrera { get; set; }
        public List<TimeSpan> tiempos { get; set; }
        public TimeSpan tiempoFinal { get; set; }
    }
}
