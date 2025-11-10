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
                CarreraId = idCarrera,
                NumeroDorsal = carrera.Inscripciones.Count + 101,
                ChipId = atleta.ChipID
            };
            _context.Inscripciones.Add(ins);
            atleta.Inscripciones.Add(ins);
            carrera.Inscripciones.Add(ins);
            _context.Atletas.Update(atleta);
            _context.Carreras.Update(carrera);
            await _context.SaveChangesAsync();
            return Ok("Inscripción exitosa");
        }

        [HttpGet("GetCarreras")]
        public async Task<IActionResult> GetCarreras(int idAtleta)
        {
            List<Model.Carrera> carreras = await _context.Carreras.ToListAsync();
            List<Model.Carrera> carreritas = new List<Model.Carrera>();
            Atleta atleta = await _context.Atletas
        .Include(a => a.Inscripciones)
        .ToListAsync()
        .ContinueWith(t => t.Result.FirstOrDefault(a => a.IdAtleta == idAtleta));
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

        private bool ParticipacionExists(int id)
        {
            return true;
        }
    }
}
