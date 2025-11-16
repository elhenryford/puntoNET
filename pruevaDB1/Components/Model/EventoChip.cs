namespace pruevaDB1.Components.Model
{
    public class EventoChip
    {
        public int ChipId { get; set; }
        public int PuntoControlId { get; set; }
        public TimeSpan HoraLectura { get; set; }
        public int CarreraId { get; set; }
    }
}