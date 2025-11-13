using Microsoft.AspNetCore.Mvc;
using pruevaDB1.Components.Model;

[ApiController]
[Route("api/paso")]
public class PasoController : ControllerBase
{
    private readonly QueueService _queueService;

    public PasoController(QueueService queueService)
    {
        _queueService = queueService;
    }

    [HttpPost("registrar")]
    public IActionResult RegistrarPaso([FromBody] RegistroPasoRequest request)
    {
        var evento = new EventoChip
        {
            ChipId = request.ChipId,
            PuntoControlId = request.PuntoId
        };

        _queueService.Enqueue(evento); // <-- aquí usás Enqueue del servicio
        return Ok(new { message = "Evento encolado correctamente" });
    }

    public record RegistroPasoRequest(int ChipId, int PuntoId);
}
