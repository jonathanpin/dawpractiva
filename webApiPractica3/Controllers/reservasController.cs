using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApiPractica3.Models;

namespace webApiPractica3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class reservasController : ControllerBase
    {
        private readonly equiposContext _context;

        public reservasController(equiposContext context)
        {
            _context = context;
        }

        // GET: api/reservas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<reservas>>> Getreservas()
        {
          if (_context.reservas == null)
          {
              return NotFound();
          }
            return await _context.reservas.ToListAsync();
        }

        // GET: api/reservas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<reservas>> Getreservas(int id)
        {
          if (_context.reservas == null)
          {
              return NotFound();
          }
            var reservas = await _context.reservas.FindAsync(id);

            if (reservas == null)
            {
                return NotFound();
            }

            return reservas;
        }

        // PUT: api/reservas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putreservas(int id, reservas reservas)
        {
            if (id != reservas.reserva_id)
            {
                return BadRequest();
            }

            _context.Entry(reservas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!reservasExists(id))
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

        // POST: api/reservas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<reservas>> Postreservas(reservas reservas)
        {
          if (_context.reservas == null)
          {
              return Problem("Entity set 'equiposContext.reservas'  is null.");
          }
            _context.reservas.Add(reservas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getreservas", new { id = reservas.reserva_id }, reservas);
        }

        // DELETE: api/reservas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletereservas(int id)
        {
            if (_context.reservas == null)
            {
                return NotFound();
            }
            var reservas = await _context.reservas.FindAsync(id);
            if (reservas == null)
            {
                return NotFound();
            }

            _context.reservas.Remove(reservas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool reservasExists(int id)
        {
            return (_context.reservas?.Any(e => e.reserva_id == id)).GetValueOrDefault();
        }
    }
}
