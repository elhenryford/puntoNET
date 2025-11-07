using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using pruevaDB1.Data;
using pruevaDB1.Components.Model;

public class QueueService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ConcurrentQueue<(int ChipId, int PuntoId)> _eventQueue = new();

    public QueueService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void Enqueue((int ChipId, int PuntoId) evento)
    {
        _eventQueue.Enqueue(evento);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            ProcessQueue();
            await Task.Delay(1000, stoppingToken);
        }
    }

    private void ProcessQueue()
    {
        while (_eventQueue.TryDequeue(out var evento))
        {
            Console.WriteLine($"Procesando evento -> ChipId: {evento.ChipId}, PuntoId: {evento.PuntoId}");

            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<pruevaDB1Context>();

            var inscripcion = context.Inscripciones
                .FirstOrDefault(i => i.NumeroDorsal == evento.ChipId);

            if (inscripcion == null)
            {
                Console.WriteLine($"❌ No se encontró Inscripción con NúmeroDorsal={evento.ChipId}");
                return;
            }

            Console.WriteLine($"✅ Inscripción encontrada: ID={inscripcion.IdInscripcion}");

            var tiempo = new TiempoParcial
            {
                InscripcionId = inscripcion.IdInscripcion,
                HoraPaso = DateTime.Now
            };

            context.TiemposParciales.Add(tiempo);
            context.SaveChanges();

            Console.WriteLine($"💾 Guardado TiempoParcial: {tiempo.HoraPaso} para Inscripción {tiempo.InscripcionId}");
        }
    }

}

