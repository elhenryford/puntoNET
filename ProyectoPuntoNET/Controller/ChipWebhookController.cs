using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPuntoNET.Data;
namespace ProyectoPuntoNET.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChipWebhookController : ControllerBase
    {
        private readonly NumberQueueService _queueService;

        public ChipWebhookController(NumberQueueService queueService)
        {
            _queueService = queueService;
        }

        public class ChipPayload
        {
            public string? ChipId { get; set; }
            public string? Checkpoint { get; set; }
        }

        [HttpPost("capture")]
        public IActionResult Capture([FromBody] ChipPayload payload)
        {
            if (payload == null || string.IsNullOrEmpty(payload.ChipId))
                return BadRequest();

            _queueService.Enqueue(async provider =>
            {
                var db = provider.GetRequiredService<AppDbContext>();
                var now = DateTime.UtcNow;

                // Buscar al atleta asociado al chip
                var atleta = await db.Atletas.FirstOrDefaultAsync(a => a.ChipId == payload.ChipId);

                if (atleta == null)
                {
                    // Si no se encuentra el atleta, puedes registrar el evento igual o descartarlo
                    Console.WriteLine($"⚠️ No se encontró atleta con chip {payload.ChipId}");
                    return;
                }

                var chipEvent = new ChipEvent
                {
                    ChipId = payload.ChipId,
                    Timestamp = now,
                    AthleteId = atleta.Numero, // Usamos el atleta encontrado
                    Checkpoint = payload.Checkpoint
                };

                db.ChipEvents.Add(chipEvent);
                await db.SaveChangesAsync();
            });

            return Accepted();
        }

    }
}
