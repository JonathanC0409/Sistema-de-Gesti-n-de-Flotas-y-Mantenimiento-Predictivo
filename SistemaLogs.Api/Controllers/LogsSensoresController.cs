using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaLogs.Modelos;

namespace SistemaLogs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsSensoresController : ControllerBase
    {
        private readonly App2DbContext _context;

        public LogsSensoresController(App2DbContext context)
        {
            _context = context;
        }

        // GET: api/LogsSensores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogSensor>>> GetLogSensor()
        {
            return await _context.LogsSensores.ToListAsync();
        }

        // GET: api/LogsSensores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogSensor>> GetLogSensor(int id)
        {
            var logSensor = await _context.LogsSensores.FindAsync(id);

            if (logSensor == null)
            {
                return NotFound();
            }

            return logSensor;
        }

        // PUT: api/LogsSensores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogSensor(int id, LogSensor logSensor)
        {
            if (id != logSensor.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(logSensor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogSensorExists(id))
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

        // POST: api/LogsSensores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LogSensor>> PostLogSensor(LogSensor logSensor)
        {
            // 1. Guardar el log
            _context.LogsSensores.Add(logSensor);
            await _context.SaveChangesAsync();
            //2. Crear un registro de auditoria para la creacion 
            
            // 2. Verificar condiciones para generar alerta
            if (logSensor.Kilometraje >= 150000 ||
                logSensor.EstadoMotor.Contains("Fallo", StringComparison.OrdinalIgnoreCase))
            {
                var alerta = new AlertaPredictiva
                {
                    CamionCodigo = logSensor.CamionCodigo,
                    Mensaje = "Se detectó una posible falla en el motor o exceso de kilometraje.",
                    FechaGeneracion = DateTime.UtcNow,
                    TipoAlerta = logSensor.EstadoMotor.Contains("Fallo", StringComparison.OrdinalIgnoreCase)
                        ? "FalloMotor"
                        : "Kilometraje"
                };

                // 3. Guardar la alerta en la tabla
                _context.AlertasPredictivas.Add(alerta);
                await _context.SaveChangesAsync();
            }

            // 4. Retornar el log creado
            return CreatedAtAction("GetLogSensor", new { id = logSensor.Codigo }, logSensor);
        }


        private bool LogSensorExists(int id)
        {
            return _context.LogsSensores.Any(e => e.Codigo == id);
        }
    }
}
