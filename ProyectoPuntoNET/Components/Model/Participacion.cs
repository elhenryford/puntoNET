namespace ProyectoPuntoNET.Model
{
    public class Participacion
    {
        public int idParticipacion { get; set; }
        public Atleta atleta { get; set; }
        public Carrera carrera { get; set; }
        public List<TimeSpan> tiempos { get; set; }
        public TimeSpan tiempoFinal { get; set; }
    }
}
