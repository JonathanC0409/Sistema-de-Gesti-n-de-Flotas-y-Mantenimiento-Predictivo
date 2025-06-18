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
    public class AlertasPredictivasController : ControllerBase
    {
        private readonly App2DbContext _context;

        public AlertasPredictivasController(App2DbContext context)
        {
            _context = context;
        }

        // GET: api/AlertasPredictivas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertaPredictiva>>> GetAlertaPredictiva()
        {
            return await _context.AlertasPredictivas.ToListAsync();
        }

        // GET: api/AlertasPredictivas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertaPredictiva>> GetAlertaPredictiva(int id)
        {
            var alertaPredictiva = await _context.AlertasPredictivas.FindAsync(id);

            if (alertaPredictiva == null)
            {
                return NotFound();
            }

            return alertaPredictiva;
        }

        // PUT: api/AlertasPredictivas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlertaPredictiva(int id, AlertaPredictiva alertaPredictiva)
        {
            if (id != alertaPredictiva.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(alertaPredictiva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertaPredictivaExists(id))
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

        // POST: api/AlertasPredictivas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlertaPredictiva>> PostAlertaPredictiva(AlertaPredictiva alertaPredictiva)
        {
            _context.AlertasPredictivas.Add(alertaPredictiva);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlertaPredictiva", new { id = alertaPredictiva.Codigo }, alertaPredictiva);
        }

        // DELETE: api/AlertasPredictivas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlertaPredictiva(int id)
        {
            var alertaPredictiva = await _context.AlertasPredictivas.FindAsync(id);
            if (alertaPredictiva == null)
            {
                return NotFound();
            }

            _context.AlertasPredictivas.Remove(alertaPredictiva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlertaPredictivaExists(int id)
        {
            return _context.AlertasPredictivas.Any(e => e.Codigo == id);
        }
    }
}
