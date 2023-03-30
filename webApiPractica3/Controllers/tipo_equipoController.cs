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
    public class tipo_equipoController : ControllerBase
    {
        private readonly equiposContext _context;

        public tipo_equipoController(equiposContext context)
        {
            _context = context;
        }

        // GET: api/tipo_equipo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tipo_equipo>>> Gettipo_equipo()
        {
          if (_context.tipo_equipo == null)
          {
              return NotFound();
          }
            return await _context.tipo_equipo.ToListAsync();
        }

        // GET: api/tipo_equipo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tipo_equipo>> Gettipo_equipo(int id)
        {
          if (_context.tipo_equipo == null)
          {
              return NotFound();
          }
            var tipo_equipo = await _context.tipo_equipo.FindAsync(id);

            if (tipo_equipo == null)
            {
                return NotFound();
            }

            return tipo_equipo;
        }

        // PUT: api/tipo_equipo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Puttipo_equipo(int id, tipo_equipo tipo_equipo)
        {
            if (id != tipo_equipo.id_tipo_equipo)
            {
                return BadRequest();
            }

            _context.Entry(tipo_equipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipo_equipoExists(id))
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

        // POST: api/tipo_equipo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tipo_equipo>> Posttipo_equipo(tipo_equipo tipo_equipo)
        {
          if (_context.tipo_equipo == null)
          {
              return Problem("Entity set 'equiposContext.tipo_equipo'  is null.");
          }
            _context.tipo_equipo.Add(tipo_equipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Gettipo_equipo", new { id = tipo_equipo.id_tipo_equipo }, tipo_equipo);
        }

        // DELETE: api/tipo_equipo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletetipo_equipo(int id)
        {
            if (_context.tipo_equipo == null)
            {
                return NotFound();
            }
            var tipo_equipo = await _context.tipo_equipo.FindAsync(id);
            if (tipo_equipo == null)
            {
                return NotFound();
            }

            _context.tipo_equipo.Remove(tipo_equipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tipo_equipoExists(int id)
        {
            return (_context.tipo_equipo?.Any(e => e.id_tipo_equipo == id)).GetValueOrDefault();
        }
    }
}
