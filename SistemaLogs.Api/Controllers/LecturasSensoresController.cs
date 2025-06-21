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
    public class LecturasSensoresController : ControllerBase
    {
        private readonly App2DbContext _context;
        private readonly App1DbContext _contextSql;

        public LecturasSensoresController(App2DbContext context, App1DbContext contextSql)
        {
            _context = context;
            _contextSql = contextSql;
        }

        // GET: api/LecturasSensores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LecturaSensor>>> GetLecturaSensores()
        {
            var datos = await _context.LecturasSensores.ToListAsync();
            return datos;
        }


        // GET: api/LecturasSensores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LecturaSensor>> GetLecturaSensor(int id)
        {
            var lecturaSensor = await _context.LecturasSensores.FindAsync(id);

            if (lecturaSensor == null)
            {
                return NotFound();
            }

            return lecturaSensor;
        }

        // PUT: api/LecturasSensores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLecturaSensor(int id, LecturaSensor lecturaSensor)
        {
            if (id != lecturaSensor.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(lecturaSensor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturaSensorExists(id))
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

        // POST: api/LecturasSensores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LecturaSensor>> PostLecturaSensor(LecturaSensor lecturaSensor)
        {
            // Verificar si el CamionCodigo existe en la base de datos
            var camion = await _contextSql.Camiones.FindAsync(lecturaSensor.CamionCodigo);
            if (camion == null)
            {
                return BadRequest("El camión especificado no existe.");
            }

            // Convertir la fecha a UTC antes de guardarla
            lecturaSensor.Fecha = lecturaSensor.Fecha.ToUniversalTime();  // Convertir a UTC


            // Agregar la nueva lectura del sensor a la base de datos
            _context.LecturasSensores.Add(lecturaSensor);

            // Variables para generar alertas y logs
            var logs = new List<LogAuditoria>();
            var alertas = new List<AlertaMantenimiento>();

            // 1. Kilometraje crítico
            if (camion.Kilometraje >= 100000)
            {
                // Crear alerta de mantenimiento
                alertas.Add(new AlertaMantenimiento
                {
                    CamionCodigo = lecturaSensor.CamionCodigo,
                    TipoMantenimiento = "Mantenimiento preventivo",
                    Descripcion = $"El camión {camion.Marca} ha alcanzado un kilometraje crítico de {camion.Kilometraje} km.",
                    FechaGeneracion = DateTime.UtcNow,
                    Resuelta = false
                });

                // Crear log de auditoría
                logs.Add(new LogAuditoria
                {
                    Accion = "Alerta de mantenimiento",
                    Entidad = "LecturaSensor",
                    EntidadCodigo = lecturaSensor.CamionCodigo,
                    Usuario = "Sistema",  // Este puede ser el usuario o 'Sistema' si es automático
                    Fecha = DateTime.UtcNow,
                    Detalles = $"El camión con marca {camion.Marca} ha alcanzado un kilometraje crítico de {camion.Kilometraje} km."
                });
            }

            // 2. Estado del motor en "Alerta"
            if (camion.EstadoMotor == "Alerta")
            {
                // Crear alerta de mantenimiento
                alertas.Add(new AlertaMantenimiento
                {
                    CamionCodigo = lecturaSensor.CamionCodigo,
                    TipoMantenimiento = "Mantenimiento preventivo",
                    Descripcion = $"El camión con marca {camion.Marca} tiene el motor en estado 'Alerta'.",
                    FechaGeneracion = DateTime.UtcNow,
                    Resuelta = false
                });

                // Crear log de auditoría
                logs.Add(new LogAuditoria
                {
                    Accion = "Alerta de mantenimiento",
                    Entidad = "Lectura Sensor",
                    EntidadCodigo = lecturaSensor.CamionCodigo,
                    Usuario = "Sistema",
                    Fecha = DateTime.UtcNow,
                    Detalles = $"El camión con marca {camion.Marca} tiene el motor en estado 'Alerta'."
                });
            }

            // 3. Bajo nivel de gasolina
            if (camion.NivelGasolina < 15)  // Umbral de bajo nivel de gasolina
            {
                // Crear alerta de mantenimiento
                alertas.Add(new AlertaMantenimiento
                {
                    CamionCodigo = lecturaSensor.CamionCodigo,
                    TipoMantenimiento = "Mantenimiento preventivo",
                    Descripcion = $"El camión con marca {camion.Marca} tiene un bajo nivel de gasolina ({camion.NivelGasolina}%).",
                    FechaGeneracion = DateTime.UtcNow,
                    Resuelta = false
                });

                // Crear log de auditoría
                logs.Add(new LogAuditoria
                {
                    Accion = "Alerta de bajo nivel de gasolina",
                    Entidad = "LecturaSensor",
                    EntidadCodigo = lecturaSensor.CamionCodigo,
                    Usuario = "Sistema",
                    Fecha = DateTime.UtcNow,
                    Detalles = $"El camión con marca {camion.Marca} tiene un bajo nivel de gasolina ({camion.NivelGasolina}%)."
                });
            }

            // 4. Bajo nivel de aceite
            if (camion.NivelAceite < 20)  // Umbral de bajo nivel de aceite
            {
                // Crear alerta de mantenimiento
                alertas.Add(new AlertaMantenimiento
                {
                    CamionCodigo = lecturaSensor.CamionCodigo,
                    TipoMantenimiento = "Mantenimiento preventivo",
                    Descripcion = $"El camión con código {camion.Marca} tiene un bajo nivel de aceite ({camion.NivelAceite}%).",
                    FechaGeneracion = DateTime.UtcNow,
                    Resuelta = false
                });

                // Crear log de auditoría
                logs.Add(new LogAuditoria
                {
                    Accion = "Alerta de bajo nivel de aceite",
                    Entidad = "LecturaSensor",
                    EntidadCodigo = lecturaSensor.CamionCodigo,
                    Usuario = "Sistema",
                    Fecha = DateTime.UtcNow,
                    Detalles = $"El camión con marca {camion.Marca} tiene un bajo nivel de aceite ({camion.NivelAceite}%)."
                });
            }

            // Guardar las alertas y los logs de auditoría
            _context.AlertasMantenimientos.AddRange(alertas);
            _context.LogsAuditorias.AddRange(logs);
            await _context.SaveChangesAsync();

            // Retornar la respuesta con la nueva lectura de sensor registrada
            return CreatedAtAction("GetLecturaSensor", new { id = lecturaSensor.Codigo }, lecturaSensor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecturaSensor(int id)
        {
            var lectura = await _context.LecturasSensores.FindAsync(id);
            if (lectura == null)
            {
                return NotFound();
            }

            _context.LecturasSensores.Remove(lectura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LecturaSensorExists(int id)
        {
            return _context.LecturasSensores.Any(e => e.Codigo == id);
        }
    }
}
