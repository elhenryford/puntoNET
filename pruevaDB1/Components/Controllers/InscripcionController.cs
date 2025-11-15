using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruevaDB1.Components.Model;
using pruevaDB1.Data;
using System.Collections.Concurrent;
using static pruevaDB1.Components.Pages.AtletaPages.Inscribirse;

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

        /*esto es asi xq en login no puedo llevarlo a inscribirse lo tengo 
          que dejar en login para que pruebe denuevo*/
         [HttpPost("Inscribirse")]
          public async Task<IActionResult> Inscribirse(int idAtleta, int idCarrera) 
        { 
            Atleta atleta = await _context.Atletas
                .Include(a => a.Inscripciones)
                .FirstOrDefaultAsync(a => a.IdAtleta == idAtleta);

            var carrera = await _context.Carreras
                .Include(c => c.Inscripciones)
                .FirstOrDefaultAsync(c => c.IdCarrera == idCarrera);

            Inscripcion ins = new Inscripcion
            {
                AtletaId = idAtleta,
                CarreraId = idCarrera,
            };
            _context.Inscripciones.Add(ins);
            carrera.Inscripciones.Add(ins);
            atleta.Inscripciones.Add(ins);
            await _context.SaveChangesAsync();
            return Ok("Inscripción exitosa");
          }

        [HttpGet("GetCarreras")]
        public async Task<IActionResult> GetCarreras(int idAtleta)
        {
            var carreras = await _context.Carreras
                .Include(c => c.Inscripciones)
                .ToListAsync();

            var atleta = await _context.Atletas
                .Include(a => a.Inscripciones)
                .FirstOrDefaultAsync(a => a.IdAtleta == idAtleta);

            List<Model.Carrera> carreritas = new();

            foreach (var car in carreras)
            {
                if (car.Fecha > DateTime.Now && car.Cupos > car.Inscripciones.Count)
                {
                    bool yaInscripto = atleta.Inscripciones
                        .Any(ins => ins.CarreraId == car.IdCarrera);

                    if (!yaInscripto)
                    {
                        carreritas.Add(car);
                    }
                }
            }

            return Ok(carreritas);
        }

        [HttpPost("DarNumero")]
        public async Task<IActionResult> DarNumero(int idAtleta, int idCarrera)
        {
            var carrera = await _context.Carreras
                .Include(c => c.Inscripciones)
                .FirstOrDefaultAsync(c => c.IdCarrera == idCarrera);
            Inscripcion ins = carrera?.Inscripciones
                .FirstOrDefault(i => i.AtletaId == idAtleta);
            ins.NumeroDorsal = carrera.Inscripciones.Count + 101;
            ins.ChipId = ins.NumeroDorsal;
            await _context.SaveChangesAsync();
            return Ok();
        }


        private static readonly ConcurrentDictionary<int, SemaphoreSlim> _locks = new();
        [HttpPost("LlamarSensor")]
        public async Task<IActionResult> LlamarSensorLocked(int idChip, int idCarrera)
        {
            var sem = _locks.GetOrAdd(idChip, _ => new SemaphoreSlim(1, 1));

            await sem.WaitAsync();
            try
            {
                return await LlamarSensor(idChip, idCarrera);
            }
            finally
            {
                sem.Release();
            }
        }

        public async Task<IActionResult> LlamarSensor(int idChip, int idCarrera)
        {
            var carrrera = await _context.Carreras.FindAsync(idCarrera);
            Inscripcion inscripcion = null;
            //encuentra la inscripcion del chip
            foreach (var ins in carrrera.Inscripciones)
            {
                if (ins.ChipId == idChip)
                {
                    inscripcion = ins;
                    break;
                }
            }
            //crea el tiempo
            TiempoParcial tp = new TiempoParcial
            {
                InscripcionId = inscripcion.IdInscripcion,
                Puesto = inscripcion.TiemposParciales.Count + 1,
                NumeroDorsal = inscripcion.NumeroDorsal,
                ChipID = inscripcion.ChipId,
                HoraPaso = DateTime.Now - carrrera.HoraInicio
            };
            //await _context.TiempoParciales.AddAsync(tp);
            inscripcion.TiemposParciales.Add(tp);
            //si fue el ultimo sensor, resuelve lo del puesto
            if (tp.Puesto == carrrera.cantSensores)
            {
                if (carrrera.inscGanador == null)
                {
                    carrrera.inscGanador = inscripcion.IdInscripcion;
                    //cartel de ganador
                }
                inscripcion.TiempoTotal = tp.HoraPaso;
                int pos= 0;
                foreach (var ins in carrrera.Inscripciones)
                {
                    if(ins.Posicion > pos)
                    {
                        pos = ins.Posicion;
                    }
                }
                inscripcion.Posicion = pos + 1;
            }

            await _context.SaveChangesAsync();
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
