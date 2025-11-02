using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pruevaDB1.Components.Model;
using pruevaDB1.Data;

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
            List<Carrera> carreras = await _context.Carrera.ToListAsync();
            //Atleta atleta = await _context.Atleta.fi
            return Ok(carreras);
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
