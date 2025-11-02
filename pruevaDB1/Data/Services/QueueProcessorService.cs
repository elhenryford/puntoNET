using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pruevaDB1.Data.Models;

namespace pruevaDB1.Data.Services
{
    public class QueueProcessorService : BackgroundService
    {
        private readonly QueueService _queue;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<QueueProcessorService> _logger;

        public QueueProcessorService(QueueService queue, IServiceProvider serviceProvider, ILogger<QueueProcessorService> logger)
        {
            _queue = queue;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("QueueProcessor iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_queue.TryDequeue(out EventoChip? evento) && evento != null)
                {
                    try
                    {
                        // 🔹 Crear un nuevo scope para obtener un DbContext válido
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var context = scope.ServiceProvider.GetRequiredService<SportEventContext>();

                            var inscripcion = context.Inscripciones.FirstOrDefault(i => i.NumeroDorsal == evento.ChipId);
                            if (inscripcion != null)
                            {
                                var tiempo = new TiempoParcial
                                {
                                    InscripcionId = inscripcion.IdInscripcion,
                                    PuntoControlId = evento.PuntoControlId,
                                    HoraPaso = evento.HoraLectura
                                };
                                context.TiemposParciales.Add(tiempo);
                                await context.SaveChangesAsync(stoppingToken);

                                _logger.LogInformation($"✅ Registrado paso chip {evento.ChipId} en punto {evento.PuntoControlId}");
                            }
                            else
                            {
                                _logger.LogWarning($"⚠️ No se encontró inscripción para chip {evento.ChipId}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "❌ Error procesando evento de chip");
                    }
                }
                else
                {
                    await Task.Delay(500, stoppingToken);
                }
            }
        }
    }
}
