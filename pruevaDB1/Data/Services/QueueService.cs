using System.Collections.Concurrent;
using pruevaDB1.Data.Models;

namespace pruevaDB1.Data.Services
{
    public class QueueService
    {
        private readonly ConcurrentQueue<EventoChip> _queue = new();
        public void Enqueue(EventoChip evento) => _queue.Enqueue(evento);
        public bool TryDequeue(out EventoChip? evento) => _queue.TryDequeue(out evento);
        public int Count => _queue.Count;
    }
}