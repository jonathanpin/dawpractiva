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
    public class estados_equipoController : ControllerBase
    {
        private readonly equiposContext _context;

        public estados_equipoController(equiposContext context)
        {
            _context = context;
        }

        // GET: api/estados_equipo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<estados_equipo>>> Getestados_equipo()
        {
          if (_context.estados_equipo == null)
          {
              return NotFound();
          }
            return await _context.estados_equipo.ToListAsync();
        }

        // GET: api/estados_equipo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<estados_equipo>> Getestados_equipo(int id)
        {
          if (_context.estados_equipo == null)
          {
              return NotFound();
          }
            var estados_equipo = await _context.estados_equipo.FindAsync(id);

            if (estados_equipo == null)
            {
                return NotFound();
            }

            return estados_equipo;
        }

        // PUT: api/estados_equipo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putestados_equipo(int id, estados_equipo estados_equipo)
        {
            if (id != estados_equipo.id_estados_equipo)
            {
                return BadRequest();
            }

            _context.Entry(estados_equipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!estados_equipoExists(id))
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

        // POST: api/estados_equipo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<estados_equipo>> Postestados_equipo(estados_equipo estados_equipo)
        {
          if (_context.estados_equipo == null)
          {
              return Problem("Entity set 'equiposContext.estados_equipo'  is null.");
          }
            _context.estados_equipo.Add(estados_equipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getestados_equipo", new { id = estados_equipo.id_estados_equipo }, estados_equipo);
        }

        // DELETE: api/estados_equipo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteestados_equipo(int id)
        {
            if (_context.estados_equipo == null)
            {
                return NotFound();
            }
            var estados_equipo = await _context.estados_equipo.FindAsync(id);
            if (estados_equipo == null)
            {
                return NotFound();
            }

            _context.estados_equipo.Remove(estados_equipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool estados_equipoExists(int id)
        {
            return (_context.estados_equipo?.Any(e => e.id_estados_equipo == id)).GetValueOrDefault();
        }
    }
}
