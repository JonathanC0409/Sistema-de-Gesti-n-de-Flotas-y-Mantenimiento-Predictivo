using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using SistemaGFYMP.Modelos;
using SistemaLogs.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace SistemaGFYMP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamionesController : ControllerBase
    {
        private readonly App1DbContext _context;
        private readonly HttpClient _httpClient;


        public CamionesController(App1DbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        [HttpGet("conductor/{id}")]
        public async Task<ActionResult<IEnumerable<Camion>>> GetConductores(int id)
        {
            var datos = await _context.Camiones
                .Where(c => c.ConductorCodigo == id)
                 .Include(co => co.Conductor)
                .ToListAsync();

            return datos;
        }

        // GET: api/Camiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Camion>>> GetCamiones()
        {
            var data = await _context.Camiones
                .Include(c => c.Conductor)
                .Include(c => c.MantenimientosProgramados)
                .ThenInclude(mp => mp.Taller)
                .ToListAsync();
            return data;
        }

        // GET: api/Camiones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Camion>> GetCamion(int id)
        {
            var camion = await _context.Camiones
                .Where(c => c.Codigo == id)
                .Include(c => c.Conductor)
                .FirstAsync();

            if (camion == null)
            {
                return NotFound();
            }

            return camion;
        }

        // PUT: api/Camiones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCamion(int id, Camion camion)
        {
            if (id != camion.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(camion).State = EntityState.Modified;
            

            try
            {
                await _context.SaveChangesAsync();
                var log = new LogAuditoria
                {
                    Codigo = 0, // El código se generará automáticamente al guardar
                    Accion = "Modificacion de camión",
                    Entidad = "Camion",
                    EntidadCodigo = camion.Codigo,
                    Usuario = "Sistema",  // Esto puede ser el usuario actual si es necesario
                    Fecha = DateTime.UtcNow,
                    Detalles = $"El camión marca {camion.Marca} , Año {camion.Año} placa {camion.Placa}  ha sido editado."
                };

                var logResponse = await _httpClient.PostAsJsonAsync("https://localhost:7220/api/LogsAuditorias", log);
                if (!logResponse.IsSuccessStatusCode)
                {
                    return StatusCode(500, "Error al registrar el log de auditoría.");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CamionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Camiones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Camion>> PostCamion(Camion camion)
        {
            

       
            _context.Camiones.Add(camion);
            await _context.SaveChangesAsync();

            var log = new LogAuditoria
            {
                Codigo = 0, // El código se generará automáticamente al guardar
                Accion = "Creación de camión",
                Entidad = "Camion",
                EntidadCodigo = camion.Codigo,
                Usuario = "Sistema",  // Esto puede ser el usuario actual si es necesario
                Fecha = DateTime.UtcNow,
                Detalles = $"El camión marca {camion.Marca} , Año {camion.Año} placa {camion.Placa}  ha sido creado."
            };

            var sensor = new LecturaSensor
            {
                Codigo = 0, // El código se generará automáticamente al guardar
                CamionCodigo = camion.Codigo,
                Kilometraje = camion.Kilometraje,
                EstadoMotor = camion.EstadoMotor,
                Fecha = DateTime.UtcNow, // Asignar la fecha actual
                Comentarios = $"Lectura del Sensor del camion {camion.Marca}",
                NivelGasolina = camion.NivelGasolina,
                NivelAceite = camion.NivelAceite
            };
            var logResponse = await _httpClient.PostAsJsonAsync("https://localhost:7220/api/LogsAuditorias", log);
            var sensorResponse = await _httpClient.PostAsJsonAsync("https://localhost:7220/api/LecturasSensores", sensor);
            // Verificar que ambas solicitudes fueron exitosas
            if (!logResponse.IsSuccessStatusCode)
            {
                return StatusCode(500, "Error al registrar el log de auditoría.");
            }

            if (!sensorResponse.IsSuccessStatusCode)
            {
                return StatusCode(500, "Error al registrar la lectura del sensor.");
            }

            return CreatedAtAction("GetCamion", new { id = camion.Codigo }, camion);
        }

        // DELETE: api/Camiones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCamion(int id)
        {
            var camion = await _context.Camiones
                .Where(c => c.Codigo == id)
                .Include(c => c.Conductor)
                .FirstAsync();
            if (camion == null)
            {
                return NotFound();
            }

            
            _context.Camiones.Remove(camion);
            await _context.SaveChangesAsync();
            var log = new LogAuditoria
            {
                Codigo = 0, // El código se generará automáticamente al guardar
                Accion = "Eliminacion de camión",
                Entidad = "Camion",
                EntidadCodigo = camion.Codigo,
                Usuario = "Sistema",  // Esto puede ser el usuario actual si es necesario
                Fecha = DateTime.UtcNow,
                Detalles = $"El camión marca {camion.Marca} , Año {camion.Año} placa {camion.Placa}  ha sido eliminado."
            };

            var logResponse = await _httpClient.PostAsJsonAsync("https://localhost:7220/api/LogsAuditorias", log);
            if (!logResponse.IsSuccessStatusCode)
            {
                return StatusCode(500, "Error al registrar el log de auditoría.");
            }
            return NoContent();
        }

        private bool CamionExists(int id)
        {
            return _context.Camiones.Any(e => e.Codigo == id);
        }
    }
}
