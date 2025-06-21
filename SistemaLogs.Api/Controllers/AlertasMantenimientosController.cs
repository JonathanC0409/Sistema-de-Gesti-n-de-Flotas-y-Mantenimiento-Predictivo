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
    public class AlertasMantenimientosController : ControllerBase
    {
        private readonly App2DbContext _context;
        private readonly App1DbContext _contextSql;

        public AlertasMantenimientosController(App2DbContext context, App1DbContext contextSql)
        {
            _context = context;
             _contextSql = contextSql;
        }

        // PUT: api/AlertasMantenimientos/Atender/5
        [HttpPut("Atender/{id}")]
        public async Task<IActionResult> AtenderAlertaMantenimiento(int id)
        {
            var alertaMantenimiento = await _context.AlertasMantenimientos.FindAsync(id);
            if (alertaMantenimiento == null)
            {
                return NotFound();
            }

            // Marcar la alerta como resuelta
            alertaMantenimiento.Resuelta = true;

            // Guardar los cambios
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/AlertasMantenimientos/pendiente
        [HttpGet("Pendiente")]
        public async Task<ActionResult<IEnumerable<AlertaMantenimiento>>> GetMantenimientosPendientes()
        {
            // Obtener todas las alertas de mantenimiento que no han sido resueltas
            var mantenimientosPendientes = await _context.AlertasMantenimientos
                .Where(a => a.Resuelta == false)  // Filtrar solo las alertas que no están resueltas
                .ToListAsync();  // Ejecutar la consulta asincrónicamente

            // Verificar si no se encontraron mantenimientos pendientes
            if (mantenimientosPendientes == null || !mantenimientosPendientes.Any())
            {
                return NotFound("No hay mantenimientos pendientes.");
            }

            // Devolver la lista de mantenimientos pendientes
            return Ok(mantenimientosPendientes);
        }



        // GET: api/AlertawMantenimientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertaMantenimiento>>> GetAlertaMantenimiento()
        {
            return await _context.AlertasMantenimientos.ToListAsync();
        }

        // GET: api/AlertawMantenimientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertaMantenimiento>> GetAlertaMantenimiento(int id)
        {
            var alertaMantenimiento = await _context.AlertasMantenimientos.FindAsync(id);

            if (alertaMantenimiento == null)
            {
                return NotFound();
            }

            return alertaMantenimiento;
        }

        // PUT: api/AlertawMantenimientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlertaMantenimiento(int id, AlertaMantenimiento alertaMantenimiento)
        {
            if (id != alertaMantenimiento.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(alertaMantenimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertaMantenimientoExists(id))
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

        // POST: api/AlertawMantenimientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlertaMantenimiento>> PostAlertaMantenimiento(AlertaMantenimiento alertaMantenimiento)
        {
            // Validación: Verificar si el CamionCodigo existe en la base de datos de SQL Server
            var camion = await _contextSql.Camiones.FindAsync(alertaMantenimiento.CamionCodigo);
            if (camion == null)
            {
                return BadRequest("El camión especificado no existe en la base de datos.");
            }

            // Si el camión existe, agregar la alerta de mantenimiento
            _context.AlertasMantenimientos.Add(alertaMantenimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlertaMantenimiento", new { id = alertaMantenimiento.Codigo }, alertaMantenimiento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlertaMantenimiento(int id)
        {
            var alerta = await _context.AlertasMantenimientos.FindAsync(id);
            if (alerta == null)
            {
                return NotFound();
            }

            _context.AlertasMantenimientos.Remove(alerta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

       
        private bool AlertaMantenimientoExists(int id)
        {
            return _context.AlertasMantenimientos.Any(e => e.Codigo == id);
        }
    }
}
