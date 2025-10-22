namespace ProyectoPuntoNET.Model
{
    public class Atleta
    {
        public int idAtleta { get; set; }
        public string nombre { get; set; }
        public int edad { get; set; }
        public List<string> discapacidades { get; set; }
        public List<Participacion>? participaciones { get; set; }


    }
}
