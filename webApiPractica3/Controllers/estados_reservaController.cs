using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practicaMVC05.Models;
using webApiPractica3.Models;

namespace webApiPractica3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estados_reservaController : ControllerBase
    {
        private readonly equiposContext _context;

        public estados_reservaController(equiposContext context)
        {
            _context = context;
        }

        // GET: api/estados_reserva
        [HttpGet]
        public async Task<ActionResult<IEnumerable<estados_reserva>>> Getestados_reserva()
        {
          if (_context.estados_reserva == null)
          {
              return NotFound();
          }
            return await _context.estados_reserva.ToListAsync();
        }

        // GET: api/estados_reserva/5
        [HttpGet("{id}")]
        public async Task<ActionResult<estados_reserva>> Getestados_reserva(int id)
        {
          if (_context.estados_reserva == null)
          {
              return NotFound();
          }
            var estados_reserva = await _context.estados_reserva.FindAsync(id);

            if (estados_reserva == null)
            {
                return NotFound();
            }

            return estados_reserva;
        }

        // PUT: api/estados_reserva/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putestados_reserva(int id, estados_reserva estados_reserva)
        {
            if (id != estados_reserva.estado_res_id)
            {
                return BadRequest();
            }

            _context.Entry(estados_reserva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!estados_reservaExists(id))
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

        // POST: api/estados_reserva
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<estados_reserva>> Postestados_reserva(estados_reserva estados_reserva)
        {
          if (_context.estados_reserva == null)
          {
              return Problem("Entity set 'equiposContext.estados_reserva'  is null.");
          }
            _context.estados_reserva.Add(estados_reserva);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getestados_reserva", new { id = estados_reserva.estado_res_id }, estados_reserva);
        }

        // DELETE: api/estados_reserva/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteestados_reserva(int id)
        {
            if (_context.estados_reserva == null)
            {
                return NotFound();
            }
            var estados_reserva = await _context.estados_reserva.FindAsync(id);
            if (estados_reserva == null)
            {
                return NotFound();
            }

            _context.estados_reserva.Remove(estados_reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool estados_reservaExists(int id)
        {
            return (_context.estados_reserva?.Any(e => e.estado_res_id == id)).GetValueOrDefault();
        }
    }
}
