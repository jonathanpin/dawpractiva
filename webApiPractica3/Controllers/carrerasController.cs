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
    public class carrerasController : ControllerBase
    {
        private readonly equiposContext _context;

        public carrerasController(equiposContext context)
        {
            _context = context;
        }

        // GET: api/carreras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<carreras>>> Getcarreras()
        {
          if (_context.carreras == null)
          {
              return NotFound();
          }
            return await _context.carreras.ToListAsync();
        }

        // GET: api/carreras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<carreras>> Getcarreras(int id)
        {
          if (_context.carreras == null)
          {
              return NotFound();
          }
            var carreras = await _context.carreras.FindAsync(id);

            if (carreras == null)
            {
                return NotFound();
            }

            return carreras;
        }

        // PUT: api/carreras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcarreras(int id, carreras carreras)
        {
            if (id != carreras.carrera_id)
            {
                return BadRequest();
            }

            _context.Entry(carreras).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!carrerasExists(id))
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

        // POST: api/carreras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<carreras>> Postcarreras(carreras carreras)
        {
          if (_context.carreras == null)
          {
              return Problem("Entity set 'equiposContext.carreras'  is null.");
          }
            _context.carreras.Add(carreras);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcarreras", new { id = carreras.carrera_id }, carreras);
        }

        // DELETE: api/carreras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecarreras(int id)
        {
            if (_context.carreras == null)
            {
                return NotFound();
            }
            var carreras = await _context.carreras.FindAsync(id);
            if (carreras == null)
            {
                return NotFound();
            }

            _context.carreras.Remove(carreras);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool carrerasExists(int id)
        {
            return (_context.carreras?.Any(e => e.carrera_id == id)).GetValueOrDefault();
        }
    }
}
