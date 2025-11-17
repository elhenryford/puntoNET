using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pruevaDB1.Components.Model;
using pruevaDB1.Data;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static pruevaDB1.Components.Pages.AtletaPages.Inscribirse;

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

            var carrera = await context.Carreras
                .Include(c => c.Inscripciones)
                .FirstOrDefaultAsync(c => c.IdCarrera == evento.CarreraId);
            var inscripcion = await context.Inscripciones
    .FirstOrDefaultAsync(i => i.ChipId == evento.ChipId && i.CarreraId == evento.CarreraId);
            var atleta = context.Atletas.FirstOrDefault(a => a.ChipID == evento.ChipId);
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
                HoraPaso = evento.HoraLectura - carrera.HoraInicio,
                Puesto = evento.PuntoControlId,
                Inscripcion = inscripcion,
                NumeroDorsal = atleta?.NumeroDorsal ?? 0,
                ChipID = evento.ChipId,

            };
            //Da null en este if
            Console.WriteLine($"1 - carrera.cantSensores = {carrera?.cantSensores}"); ;
            if (nuevoTiempo.Puesto == carrera.cantSensores)
            {
                if (carrera.inscGanador == 0)
                {
                    carrera.inscGanador = inscripcion.IdInscripcion;
                    //cartel de ganador
                }
                inscripcion.TiempoTotal = nuevoTiempo.HoraPaso;
                int pos = 0;
                foreach (var ins in carrera.Inscripciones)
                {
                    if (ins.Posicion > pos)
                    {
                        pos = ins.Posicion;
                    }
                }
                inscripcion.Posicion = pos + 1;
            }
            inscripcion.TiemposParciales.Add(nuevoTiempo);
            context.TiemposParciales.Add(nuevoTiempo);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Error guardando TiempoParcial: {ex.Message} - {ex.InnerException?.Message}");
        }
    }
}
