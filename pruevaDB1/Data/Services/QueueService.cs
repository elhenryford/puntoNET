using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using pruevaDB1.Data;
using pruevaDB1.Components.Model;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public class QueueService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ConcurrentQueue<EventoChip> _queue = new(); // cola interna

    public QueueService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    // Método público para encolar desde controladores u otros servicios
    public void Enqueue(EventoChip evento)
    {
        if (evento == null) throw new ArgumentNullException(nameof(evento));
        _queue.Enqueue(evento);
    }
    public EventoChip? Dequeue()
    {
        return _queue.TryDequeue(out var evento) ? evento : null;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("QueueService iniciado.");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (_queue.TryDequeue(out var evento))
                {
                    await ProcesarEventoAsync(evento, stoppingToken);
                }
                else
                {
                    // Si no hay eventos, espera breve para no busy-wait
                    await Task.Delay(200, stoppingToken);
                }
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                Console.WriteLine($"QueueService error general: {ex.Message} - {ex.InnerException?.Message}");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }

    private async Task ProcesarEventoAsync(EventoChip evento, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<pruevaDB1Context>();
        

        try
        {
            Console.WriteLine($"🔄 Procesando evento -> ChipId: {evento.ChipId}");

            var inscripcion = context.Inscripciones.FirstOrDefault(i => i.ChipId == evento.ChipId);

            if (inscripcion == null)
            {
                Console.WriteLine($" No se encontró Inscripción con ChipId={evento.ChipId}");
                return;
            }

            if (evento.HoraLectura == default)
            {
                Console.WriteLine(" HoraLectura inválida. Se omite.");
                return;
            }

            var nuevoTiempo = new TiempoParcial
            {
                InscripcionId = inscripcion.IdInscripcion,
                HoraPaso = evento.HoraLectura,
                Puesto = evento.PuntoControlId,
                Inscripcion = inscripcion,
                ChipID = evento.ChipId,
                NumeroDorsal = inscripcion.NumeroDorsal
            };
            inscripcion.TiemposParciales.Add(nuevoTiempo);
            // Usar el DbSet que coincide con tu tabla: "TiempoParcial"
            context.TiempoParcial.Add(nuevoTiempo);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Error guardando TiempoParcial: {ex.Message} - {ex.InnerException?.Message}");
        }
    }
}
