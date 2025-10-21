using System.Collections.Generic;

namespace ProyectoPuntoNET.Models
{
    public class Atleta
    {
        public int Numero { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public List<string> Discapacidades { get; set; } = new List<string>();
    }
}
