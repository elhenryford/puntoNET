using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruevaDB1.Components.Model;
using pruevaDB1.Data;
using static pruevaDB1.Components.Pages.AtletaPages.Inscribirse;

namespace pruevaDB1.Components.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipacionController : ControllerBase
    {
        private readonly pruevaDB1Context _context;

        public ParticipacionController(pruevaDB1Context context)
        {
            _context = context;
        }

        [HttpPost("Inscribirse")]
        public async Task<IActionResult> Inscribirse(int idAtleta, int idCarrera)
        {
            var nueva = new Participacion();
            var atleta = await _context.Atleta.FindAsync(idAtleta);
            var carrera = await _context.Carrera.FindAsync(idCarrera);

            if (atleta == null || carrera == null)
                return NotFound("Atleta o carrera no encontrada.");

            nueva.Atleta = atleta;
            nueva.Carrera = carrera;

            _context.Participacion.Add(nueva);
            await _context.SaveChangesAsync();

            return Ok("Inscripción exitosa");
        }

        [HttpGet("GetCarreras")]
        public async Task<IActionResult> GetCarreras(int idAtleta)
        {
            List<Model.Carrera> carreritas = await _context.Carrera.ToListAsync();
            List<Model.Carrera> carreras = new List<Model.Carrera>();
            Atleta atleta = await _context.Atleta.FindAsync(idAtleta);
            foreach (var carrera in carreritas)
            {
                bool yaInscripto = await _context.Participacion
                    .AnyAsync(p => p.Atleta.IdAtleta == idAtleta && p.Carrera.IdCarrera == carrera.IdCarrera);
                if (!yaInscripto)
                {
                    if(carrera.Corredores.Count < carrera.CuposDisponibles) { carreras.Add(carrera); }
                }
            }
            return Ok(carreras);
        }

        [HttpPost("LlamarSensor")]
        public async Task<IActionResult> LlamarSensor(int idChip)
        {
            Participacion p = await _context.Participacion.FindAsync(idChip);
            int idAtleta = p.Atleta.IdAtleta;
            int idCarrera = p.Carrera.IdCarrera;
            foreach (var carrera in await _context.Carrera.ToListAsync()){
                if(carrera.IdCarrera == idCarrera)
                {
                    foreach (var participacion in carrera.Corredores)
                    {
                        if(participacion.Atleta.IdAtleta == idAtleta)
                        {
                            TimeSpan newTime = DateTime.Now.TimeOfDay - carrera.Fecha.TimeOfDay;
                            if (participacion.Tiempos.Count < (carrera.CantidadPuntosControl - 1))
                            {
                                participacion.Tiempos.Add(newTime);
                            }else { participacion.TiempoFinal = newTime; }
                            _context.Update(participacion);
                        }
                    }
                }
            }
            return Ok("Sensor llamado");
        }

        // POST: Particiapcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participacion = await _context.Participacion.FindAsync(id);
            if (participacion != null)
            {
                _context.Participacion.Remove(participacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipacionExists(int id)
        {
            return _context.Participacion.Any(e => e.IdParticipacion == id);
        }
    }
}
