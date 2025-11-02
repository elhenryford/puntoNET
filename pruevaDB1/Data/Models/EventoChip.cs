namespace pruevaDB1.Data.Models
{
    public class EventoChip
    {
        public int ChipId { get; set; }
        public int PuntoControlId { get; set; }
        public DateTime HoraLectura { get; set; } = DateTime.Now;
    }
}