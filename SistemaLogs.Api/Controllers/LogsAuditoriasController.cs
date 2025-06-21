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
    public class LogsAuditoriasController : ControllerBase
    {
        private readonly App2DbContext _context;
        private readonly App1DbContext _contextSql; 

        public LogsAuditoriasController(App2DbContext context, App1DbContext contextSql)
        {
            _context = context;
            _contextSql = contextSql;
        }

        [HttpGet("logs")]
        public async Task<ActionResult<IEnumerable<LogAuditoria>>> GetLogs()
        {
            var logs = await _context.LogsAuditorias.ToListAsync();
            return Ok(logs);
        }


        // GET: api/LogsAuditorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogAuditoria>>> GetLogsAuditorias()
        {
            return await _context.LogsAuditorias.ToListAsync();
        }

        // GET: api/LogsAuditorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogAuditoria>> GetLogAuditoria(int id)
        {
            var logAuditoria = await _context.LogsAuditorias.FindAsync(id);

            if (logAuditoria == null)
            {
                return NotFound();
            }

            return logAuditoria;
        }

        // PUT: api/LogsAuditorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogAuditoria(int id, LogAuditoria logAuditoria)
        {
            if (id != logAuditoria.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(logAuditoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogAuditoriaExists(id))
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

        // POST: api/LogsAuditorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LogAuditoria>> PostLogAuditoria(LogAuditoria logAuditoria)
        {
            _context.LogsAuditorias.Add(logAuditoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogAuditoria", new { id = logAuditoria.Codigo }, logAuditoria);
        }

        // DELETE: api/LogsAuditorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogAuditoria(int id)
        {
            var logAuditoria = await _context.LogsAuditorias.FindAsync(id);
            if (logAuditoria == null)
            {
                return NotFound();
            }

            _context.LogsAuditorias.Remove(logAuditoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogAuditoriaExists(int id)
        {
            return _context.LogsAuditorias.Any(e => e.Codigo == id);
        }
    }
}
