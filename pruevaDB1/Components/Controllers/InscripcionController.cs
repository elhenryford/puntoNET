using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruevaDB1.Components.Model;
using pruevaDB1.Data;
using System.Collections.Concurrent;

namespace pruevaDB1.Components.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InscripcionController : ControllerBase
    {
        private readonly pruevaDB1Context _context;

        public InscripcionController(pruevaDB1Context context)
        {
            _context = context;
        }

        [HttpPost("Inscribirse")]
        public async Task<IActionResult> Inscribirse(int idAtleta, int idCarrera)
        {
            Atleta atleta = await _context.Atletas.FindAsync(idAtleta);
            Carrera carrera = await _context.Carreras.FindAsync(idCarrera);
            Inscripcion ins = new Inscripcion
            {
                AtletaId = idAtleta,
                Atleta = atleta,
                CarreraId = idCarrera,
                Carrera = carrera,
                NumeroDorsal = carrera.Inscripciones.Count + 100
            };
            _context.Inscripciones.Add(ins);
            return Ok("Inscripción exitosa");
        }

        [HttpGet("GetCarreras")]
        public async Task<IActionResult> GetCarreras(int idAtleta)
        {
            List<Model.Carrera> carreras = await _context.Carreras.ToListAsync();
            List<Model.Carrera> carreritas = new List<Model.Carrera>();
            Atleta atleta = await _context.Atletas.FindAsync(idAtleta);
            foreach (var car in carreras)
            {
                if (car.Fecha > DateTime.Now)
                {
                    bool estaInscrito = false;
                    foreach (var ins in atleta.Inscripciones)
                    {
                        if (ins.CarreraId == car.IdCarrera)
                        {
                            estaInscrito = true;
                            break;
                        }
                    }
                    if (!estaInscrito)
                    {
                        carreritas.Add(car);
                    }
                }
            }
            return Ok(carreritas);
        }

        private static readonly ConcurrentDictionary<int, SemaphoreSlim> _locks = new();
        [HttpPost("LlamarSensor")]
        public async Task<IActionResult> LlamarSensorLocked(int idChip)
        {
            var sem = _locks.GetOrAdd(idChip, _ => new SemaphoreSlim(1, 1));

            await sem.WaitAsync();
            try
            {
                return await LlamarSensor(idChip);
            }
            finally
            {
                sem.Release();
            }
        }

        public async Task<IActionResult> LlamarSensor(int idChip)
        {
            return Ok("Sensor llamado");
        }

        // POST: Particiapcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipacionExists(int id)
        {
            return true;
        }
    }
}
