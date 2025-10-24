using System.ComponentModel.DataAnnotations;

namespace ProyectoPuntoNET.Model
{
    public class Atleta
    {
        [Key]
        public int idAtleta { get; set; }
        public string nombre { get; set; }
        public int edad { get; set; }
        public string discapacidades { get; set; }
        public List<Participacion>? participaciones { get; set; }


    }
}
